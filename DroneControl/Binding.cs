using DroneLibrary.Diagnostics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DroneControl
{
    public class Binding
    {
        public SettingsForm Settings { get; private set; }
        public Control Control { get; private set; }
        public string DataMember { get; private set; }

        private string[] memberParts;
        private PropertyInfo controlProperty;
        private bool ignoreChange;

        public event EventHandler OnChangedByUser;

        public bool ChangedByUser { get; private set; }

        public Binding(SettingsForm settings, Control control, string dataMember)
        {
            this.Settings = settings;
            this.Control = control;
            this.DataMember = dataMember;
            this.memberParts = dataMember.Split('.');

            if (control is TextBox)
            {
                controlProperty = control.GetType().GetProperty("Text");
                (control as TextBox).TextChanged += (s, e) => UpdateData();
            }
            else if (control is NumericUpDown)
            {
                controlProperty = control.GetType().GetProperty("Value");
                (control as NumericUpDown).ValueChanged += (s, e) => UpdateData();
            }
            else if (control is CheckBox)
            {
                controlProperty = control.GetType().GetProperty("Checked");
                (control as CheckBox).CheckedChanged += (s, e) => UpdateData();
            }
            else
                throw new NotSupportedException("Control type not supported");

            NotifyValueChanged();
        }

        public void ClearChangedByUser()
        {
            ChangedByUser = false;
            Control.ForeColor = SystemColors.WindowText;
        }

        private object GetDataValue()
        {
            object current = Settings;
            bool isPublic = false;

            foreach (string member in memberParts)
                current = GetValue(current, member, out isPublic);

            Control.Enabled = isPublic;
            return current;
        }

        private object GetValue(object data, string member, out bool isPublic)
        {
            Type type = data.GetType();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo field = type.GetField(member, flags);
            if (field != null)
            {
                isPublic = field.IsPublic;
                return field.GetValue(data);
            }
            PropertyInfo property = type.GetProperty(member, flags);
            if (property != null)
            {
                isPublic = property.SetMethod != null && property.SetMethod.IsPublic;
                return property.GetValue(data);
            }
            throw new ArgumentException("Member not found", "member");
        }

        public void UpdateData()
        {
            if (ignoreChange)
                return;

            try
            {
                object value = controlProperty.GetValue(Control);

                Stack<object> objects = new Stack<object>();
                objects.Push(Settings);
                foreach (string member in memberParts)
                {
                    bool isPublic;
                    objects.Push(GetValue(objects.Peek(), member, out isPublic));
                }

                objects.Pop();
                while (objects.Count > 0)
                {
                    object obj = objects.Pop();
                    SetValue(obj, memberParts[objects.Count], value);
                    value = obj;
                }

                ChangedByUser = true;
                Control.ForeColor = Color.DarkGreen;

                if (OnChangedByUser != null)
                    OnChangedByUser(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Log.Error(e);
                ForceValue();
            }
        }

        private void SetValue(object data, string member, object value)
        {
            Type type = data.GetType();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo field = type.GetField(member, flags);
            if (field != null)
            {
                field.SetValue(data, Convert.ChangeType(value, field.FieldType));
                return;
            }
            PropertyInfo property = type.GetProperty(member, flags);
            if (property != null)
            {
                property.SetValue(data, Convert.ChangeType(value, property.PropertyType));
                return;
            }
            throw new ArgumentException("Member not found", "member");
        }

        public void NotifyValueChanged()
        {
            if (ChangedByUser)
            {
                UpdateData();
                return;
            }
            if (Control.Focused)
                return;
            ForceValue();
        }

        private void ForceValue()
        {
            ignoreChange = true;

            object value = GetDataValue();
            controlProperty.SetValue(Control, Convert.ChangeType(value, controlProperty.PropertyType));
            ignoreChange = false;
        }
    }
}
