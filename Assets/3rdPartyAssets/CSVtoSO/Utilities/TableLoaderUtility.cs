namespace CSVtoSO.Utilities {

    using System;
    using System.Globalization;
    using System.Reflection;
    using UnityEngine;

    /// <summary>
    /// Class responsible for loading each input string from a record's table and returning the specified value and type
    /// </summary>
    public class TableLoaderUtility {

        #region Private Variables

        private TableDataReaderUtility tableReader;

        #endregion Private Variables

        #region Properties

        public string TableTitle => tableReader?.TableTitle;
        public int TableLenght => tableReader == null ? 0 : tableReader.RowsCount;

        #endregion Properties

        #region Constructors

        public TableLoaderUtility (TableDataReaderUtility reader) {
            tableReader = reader;
        }

        #endregion Constructors

        #region Loading Methods

        public void LoadField (ref object target, FieldInfo field, string columnName, int recordRowIndex, string columnPostFix) {
            try {
                string columnValue = tableReader.GetRecordStringAt(GetColumnName(columnName, columnPostFix), recordRowIndex);

                if(string.IsNullOrEmpty(columnValue))
                    return;

                ProcessFieldAndLoad(ref target, field, columnValue);
            }
            catch(Exception e) {
                Debug.LogErrorFormat("Error converting value in column {0} at row index {1}.\nThrown {2}", columnName, recordRowIndex + 2, e.ToString());
            }
        }

        private void ProcessFieldAndLoad (ref object target, FieldInfo field, string recordValue) {
            Type fieldType = field.FieldType;

            if(fieldType == typeof(string))
                field.SetValue(target, recordValue);
            else if(fieldType == typeof(char))
                field.SetValue(target, GetCharValue(recordValue));
            else if(fieldType == typeof(sbyte))
                field.SetValue(target, GetSignedByteValue(recordValue));
            else if(fieldType == typeof(byte))
                field.SetValue(target, GetUnsignedByteValue(recordValue));
            else if(fieldType == typeof(short))
                field.SetValue(target, GetShortValue(recordValue));
            else if(fieldType == typeof(ushort))
                field.SetValue(target, GetUnsignedShortValue(recordValue));
            else if(fieldType == typeof(int))
                field.SetValue(target, GetIntValue(recordValue));
            else if(fieldType == typeof(uint))
                field.SetValue(target, GetUnsignedIntValue(recordValue));
            else if(fieldType == typeof(long))
                field.SetValue(target, GetLongValue(recordValue));
            else if(fieldType == typeof(ulong))
                field.SetValue(target, GetUnsignedLongValue(recordValue));
            else if(fieldType == typeof(float))
                field.SetValue(target, GetFloatValue(recordValue));
            else if(fieldType == typeof(double))
                field.SetValue(target, GetDoubleValue(recordValue));
            else if(fieldType == typeof(decimal))
                field.SetValue(target, GetDecimalValue(recordValue));
            else if(fieldType.BaseType == typeof(Enum))
                field.SetValue(target, Enum.Parse(fieldType, recordValue));
            else if(fieldType == typeof(bool))
                field.SetValue(target, GetBoolValue(recordValue));
            else if(fieldType == typeof(Color))
                field.SetValue(target, GetColorValue(recordValue));
            else
                Debug.LogErrorFormat("Could not process field {0} because its type <b>- {1} -</b> is not managed", field.Name, fieldType);
        }

        #endregion Loading Methods

        #region Conversion

        internal char GetCharValue (string recordValue) {
            if(string.IsNullOrEmpty(recordValue))
                return char.MinValue;
            else {
                if(recordValue.Length > 1)
                    Debug.LogWarningFormat("Record value string <b>{0}</b> has a length > 1, only the first character will be considered", recordValue);

                return recordValue[0];
            }
        }

        internal bool GetBoolValue (string recordValue) {
            recordValue = recordValue.Replace("/", string.Empty);
            recordValue = recordValue.Replace(@"\", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return false;

            if(recordValue.Equals("yes", StringComparison.InvariantCultureIgnoreCase))
                return true;
            if(recordValue.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                return true;
            if(recordValue.Equals("1", StringComparison.InvariantCultureIgnoreCase))
                return true;

            return false;
        }

        internal float GetFloatValue (string recordValue) {
            recordValue = recordValue.Replace("/", string.Empty);
            recordValue = recordValue.Replace(@"\", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            if(recordValue.StartsWith("-")) {
                string tempString = recordValue.Remove(0, 1);
                return -float.Parse(tempString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            }
            else
                return float.Parse(recordValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
        }

        internal double GetDoubleValue (string recordValue) {
            recordValue = recordValue.Replace("/", string.Empty);
            recordValue = recordValue.Replace(@"\", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            if(recordValue.StartsWith("-")) {
                string tempString = recordValue.Remove(0, 1);
                return -double.Parse(tempString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            }
            else
                return double.Parse(recordValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
        }

        internal decimal GetDecimalValue (string recordValue) {
            recordValue = recordValue.Replace("/", string.Empty);
            recordValue = recordValue.Replace(@"\", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            if(recordValue.StartsWith("-")) {
                string tempString = recordValue.Remove(0, 1);
                return -decimal.Parse(tempString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            }
            else
                return decimal.Parse(recordValue, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
        }

        internal static sbyte GetSignedByteValue (string recordValue) {
            recordValue = recordValue.Replace("/", string.Empty);
            recordValue = recordValue.Replace(@"\", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            return sbyte.Parse(recordValue);
        }

        internal static short GetShortValue (string recordValue) {
            recordValue = recordValue.Replace("/", string.Empty);
            recordValue = recordValue.Replace(@"\", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            return short.Parse(recordValue);
        }

        internal static int GetIntValue (string recordValue) {
            recordValue = recordValue.Replace("/", string.Empty);
            recordValue = recordValue.Replace(@"\", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            return int.Parse(recordValue, NumberStyles.Integer);
        }

        internal static long GetLongValue (string recordValue) {
            recordValue = recordValue.Replace("/", string.Empty);
            recordValue = recordValue.Replace(@"\", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            return long.Parse(recordValue, NumberStyles.Integer);
        }

        internal static byte GetUnsignedByteValue (string recordValue) {
            recordValue = recordValue.Replace(@"\", string.Empty);
            recordValue = recordValue.Replace("/", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            if(recordValue.StartsWith("-")) {
                Debug.LogErrorFormat("Input value was negative( <b>{0}</b> ), defaulting to 0", recordValue);
                return 0;
            }

            return byte.Parse(recordValue);
        }

        internal static ushort GetUnsignedShortValue (string recordValue) {
            recordValue = recordValue.Replace(@"\", string.Empty);
            recordValue = recordValue.Replace("/", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            if(recordValue.StartsWith("-")) {
                Debug.LogErrorFormat("Input value was negative( <b>{0}</b> ), defaulting to 0", recordValue);
                return 0;
            }

            return ushort.Parse(recordValue);
        }

        internal static uint GetUnsignedIntValue (string recordValue) {
            recordValue = recordValue.Replace(@"\", string.Empty);
            recordValue = recordValue.Replace("/", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            if(recordValue.StartsWith("-")) {
                Debug.LogErrorFormat("Input value was negative( <b>{0}</b> ), defaulting to 0", recordValue);
                return 0;
            }

            return uint.Parse(recordValue);
        }

        internal static ulong GetUnsignedLongValue (string recordValue) {
            recordValue = recordValue.Replace(@"\", string.Empty);
            recordValue = recordValue.Replace("/", string.Empty);

            if(string.IsNullOrEmpty(recordValue))
                return 0;

            if(recordValue.StartsWith("-")) {
                Debug.LogErrorFormat("Input value was negative( <b>{0}</b> ), defaulting to 0", recordValue);
                return 0;
            }

            return ulong.Parse(recordValue);
        }

        internal static Color GetColorValue (string recordValue) {
            if(ColorUtility.TryParseHtmlString(recordValue, out Color convertedColor))
                return convertedColor;
            else
                return Color.black;
        }

        #endregion Conversion

        #region Loader Utilities

        public bool HasColumn (string columnName, string postFix) {
            string fullName = columnName;

            if(!string.IsNullOrEmpty(postFix))
                fullName += postFix;

            return tableReader.HasColumn(fullName);
        }

        public string GetColumnName (string columnName, string postfix) {
            string fullName = columnName;

            if(!string.IsNullOrEmpty(postfix))
                fullName += postfix;

            return fullName;
        }

        public string GetStringAt (string columnName, int row, string postFix) {
            return tableReader.GetRecordStringAt(GetColumnName(columnName, postFix), row);
        }

        #endregion Loader Utilities

        /// <summary>
        /// Used to create a value for an array's field that has to be inserted in it.
        /// </summary>
        /// <returns>The parsed value matching the input value and the type.</returns>
        public object CreateField (Type fieldType, string fieldValue) {
            if(fieldType == typeof(string))
                return fieldValue;

            if(fieldType == typeof(char))
                return GetCharValue(fieldValue);
            else if(fieldType == typeof(sbyte))
                return GetSignedByteValue(fieldValue);
            else if(fieldType == typeof(byte))
                return GetUnsignedByteValue(fieldValue);
            else if(fieldType == typeof(short))
                return GetShortValue(fieldValue);
            else if(fieldType == typeof(ushort))
                return GetUnsignedShortValue(fieldValue);
            else if(fieldType == typeof(int))
                return GetIntValue(fieldValue);
            else if(fieldType == typeof(uint))
                return GetUnsignedIntValue(fieldValue);
            else if(fieldType == typeof(long))
                return GetLongValue(fieldValue);
            else if(fieldType == typeof(ulong))
                return GetUnsignedLongValue(fieldValue);
            else if(fieldType == typeof(float))
                return GetFloatValue(fieldValue);
            else if(fieldType == typeof(double))
                return GetDoubleValue(fieldValue);
            else if(fieldType == typeof(decimal))
                return GetDecimalValue(fieldValue);
            else if(fieldType == typeof(bool))
                return GetBoolValue(fieldValue);
            else if(fieldType.BaseType == typeof(Enum))
                return Enum.Parse(fieldType, fieldValue);
            else if(fieldType == typeof(Color))
                return GetColorValue(fieldValue);
            else
                Debug.LogErrorFormat("Field Type {0} is not supported", fieldType);

            return null;
        }
    }
}