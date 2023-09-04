namespace CSVtoSO.Attributes {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CSVtoSO.Utilities;
    using UnityEngine;

    /// <summary>
    /// Base Tag to apply to fields that should have a reference to the corresponding database's table.<br/><br/>
    /// Valid for <see cref="string"/>, <see cref="int"/>, <see cref="float"/>, <see cref="Enum"/>, <see cref="bool"/> and <see cref="Color"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class TableColumnMapperAttribute : Attribute {
        public string columnName;

        /// <summary>
        /// Construct the attribute specifying which column of the database it should match to. <br/><br/>
        /// By not specifying a <b>Column Name</b> when using this tag, the name of the field is going to be used.
        /// </summary>
        /// <param name="columnName">The corresponding column name, default is <see cref="null"/></param>
        public TableColumnMapperAttribute (string columnName = null) {
            this.columnName = columnName;
        }

        /// <summary>
        /// Loads the value of the specific instance's field leaning on the utility field loader.
        /// </summary>
        /// <param name="instance">The object that is target of the field's loading.</param>
        /// <param name="fieldInfo">The representation of the specific field that has to be loaded.</param>
        /// <param name="loader">The utility that provides the actual connection to the field's loading.</param>
        /// <param name="rowIndex">The index of the table's row that this object should represent.</param>
        /// <param name="postFix">The eventual postfix to put at the end of a column's name (might be useful for internal styling and conventions).</param>
        public virtual void LoadField (ref object instance, FieldInfo fieldInfo, TableLoaderUtility loader, int rowIndex, string postFix) {
            string column = columnName;
            if(string.IsNullOrEmpty(column))
                column = fieldInfo.Name;

            try {
                if(fieldInfo.FieldType.IsArray) {
                    List<string> recordValues = new List<string>();

                    if(!loader.HasColumn(column, postFix))
                        return;

                    string recordValue = loader.GetStringAt(column, rowIndex, postFix);

                    if(!string.IsNullOrEmpty(recordValue))
                        recordValues = recordValue.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    Type arrayType = fieldInfo.FieldType;
                    Type fieldType = arrayType.GetElementType();

                    Array arrayInstance = Array.CreateInstance(fieldType, recordValues.Count);

                    for(int i = 0; i < recordValues.Count; i++)
                        arrayInstance.SetValue(loader.CreateField(fieldType, recordValues[i]), i);

                    fieldInfo.SetValue(instance, arrayInstance);
                }
                else {
                    if(!loader.HasColumn(column, postFix))
                        return;

                    loader.LoadField(ref instance, fieldInfo, column, rowIndex, postFix);
                }
            }
            catch(Exception e) {
                Debug.LogErrorFormat("Cannot convert element for column <b>{0}</b> at row <b>{1}</b> in table {2}.\nThrown {3}", loader.TableTitle, column, rowIndex + 2, e.ToString());
            }
        }
    }
}