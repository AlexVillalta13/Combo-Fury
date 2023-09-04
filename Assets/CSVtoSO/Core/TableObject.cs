namespace CSVtoSO.Core {

    using System;
    using System.Reflection;
    using CSVtoSO.Attributes;
    using CSVtoSO.Utilities;

    /// <summary>
    /// Base class to inherit from in order to represent a set of values for a specific database.<br/><br/>
    /// </summary>
    public class TableObject {

        /// <summary>
        /// Setup the necessary for loading all the fields of the corresponding <see cref="TableObject"/>'s representation.
        /// </summary>
        /// <param name="loader">The specific instance of the utility for running all the controls to load fields properly.</param>
        /// <param name="rowIndex">The index of the row corresponding to this implementation of it in the database table.</param>
        /// <param name="postFix">The eventual postfix to put at the end of a column's name (might be useful for internal styling and conventions).</param>
        internal virtual void ReadRow (TableLoaderUtility loader, int rowIndex, string postFix) {
            Type tableType = this.GetType();

            object obj = this;

            LoadRowFields(tableType, ref obj, loader, rowIndex, postFix);
        }

        private static void LoadRowFields (Type tableType, ref object instance, TableLoaderUtility loader, int rowIndex, string postFix) {
            FieldInfo[] rowFields = tableType.GetFields();

            for(int i = 0; i < rowFields.Length; i++) {
                TableColumnMapperAttribute mapperAttribute = (TableColumnMapperAttribute)Attribute.GetCustomAttribute(rowFields[i], typeof(TableColumnMapperAttribute));

                if(mapperAttribute != null) {
                    mapperAttribute.LoadField(ref instance, rowFields[i], loader, rowIndex, postFix);
                }
            }
        }
    }
}