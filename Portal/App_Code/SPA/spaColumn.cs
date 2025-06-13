using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SPA
{
    [DataContract]
    public class spaColumn
    {
        private string m_Table;
        private string m_Field;
        private string m_Type;
        private string m_Null;
        private string m_Key;
        private string m_Default;
        private string m_Extra;

        [DataMember]
        public string Table
        {
            get { return m_Table; }
            set { m_Table = value; }
        }

        [DataMember]
        public string Field
        {
            get { return m_Field; }
            set { m_Field = value; }
        }

        [DataMember]
        public string Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        [DataMember]
        public string Null
        {
            get { return m_Null; }
            set { m_Null = value; }
        }

        [DataMember]
        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        [DataMember]
        public string Default
        {
            get { return m_Default; }
            set { m_Default = value; }
        }

        [DataMember]
        public string Extra
        {
            get { return m_Extra; }
            set { m_Extra = value; }
        }

    }
}
