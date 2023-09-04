namespace CSVtoSO.Utilities {

    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Class that provides the necessary for inspecting the database's representation, running basic checks to correctly load values
    /// </summary>
    public class TableDataReaderUtility {

        #region Private Variables

        private string tableTitle;

        private Dictionary<string, int> columnMap;
        private List<string> columnNames;
        private IList<IList<object>> values;

        #endregion Private Variables

        #region Properties

        /// <summary>
        /// The total rows of the table
        /// </summary>
        public int RowsCount => values.Count - 1;

        /// <summary>
        /// The total columns of the table
        /// </summary>
        public int ColumnsCount => columnMap.Count;

        /// <summary>
        /// The name of the actual .csv file representing the table
        /// </summary>
        public string TableTitle => tableTitle;

        /// <summary>
        /// The generated matrix representation of all the cells of the table using object type.<br/><br/>
        /// The inner layer represents rows, while the outer represents columns.
        /// </summary>
        public IList<IList<object>> TableRawData => values;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructs a new instance of the utility for a specific table.
        /// </summary>
        /// <param name="tableTitle">The name of the .csv file representing the table.</param>
        /// <param name="values">The actual representation of the table using a matrix of <see cref="object"/>s as records.</param>
        /// <exception cref="Exception">Throws exception if the <param name="values"> is either empty or null, or if a column is already present in the table.</exception>
        public TableDataReaderUtility (string tableTitle, IList<IList<object>> values) {
            this.tableTitle = tableTitle;
            this.values = values;

            if(this.values == null)
                throw new Exception("Values cannot be null.");
            if(this.values.Count == 0)
                throw new Exception("Cannot have 0 amount of rows.");

            columnMap = new Dictionary<string, int>();
            columnNames = new List<string>();

            var firstRow = values[0];

            for(int i = 0; i < firstRow.Count; i++) {
                string recordName = firstRow[i].ToString();
                string rKey = recordName.ToLower();

                if(columnMap.ContainsKey(rKey))
                    throw new Exception(string.Format("Key {0} already present in the table", rKey));

                columnMap.Add(rKey, i);
                columnNames.Add(recordName);
            }
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Given the row index and the column name, returns the object representation of the value that was read in the record.
        /// </summary>
        /// <returns>The object representing the value of the record.</returns>
        /// <exception cref="Exception">Throws exceptions if it cannot either found a column by its name or a row by its index.<br/><br/>
        /// An exception is thrown also if the row representation results null.</exception>
        private object ReadRecord (string columnName, int rowIndex) {
            string fullName = columnName;

            if(!columnMap.ContainsKey(fullName.ToLower()))
                throw new Exception(string.Format("Column {0} does not exist.", fullName));

            if(rowIndex >= values.Count - 1)
                throw new Exception(string.Format("Row with index {0} does not exist.", rowIndex));

            var rowValues = values[rowIndex + 1]; //The +1 excludes the first row of the table, which should contain columns' labels

            if(rowValues == null)
                throw new Exception(string.Format("Row {0} resulted null", rowIndex));

            int column = columnMap[fullName.ToLower()];

            if(column >= rowValues.Count)
                return null;

            return rowValues[column];
        }

        #endregion Private Methods

        #region Internal Methods

        /// <summary>
        /// Provides the necessary to get the string's representation of the record specified by its corresponding column's name and row index.
        /// </summary>
        /// <returns>The string representation of the value that was read in the record.</returns>
        internal string GetRecordStringAt (string columnName, int row) {
            object colValue = ReadRecord(columnName, row);

            string returnValue = null;

            if(colValue != null)
                returnValue = colValue.ToString();

            return returnValue;
        }

        internal bool HasColumn (string columnName) {
            return columnMap.ContainsKey(columnName.ToLower());
        }

        internal string GetColumnName (int columnIndex) {
            if(columnIndex < 0 || columnIndex >= columnNames.Count)
                Debug.LogErrorFormat("No exixting column for index: {0}", columnIndex);

            return columnNames[columnIndex];
        }

        #endregion Internal Methods
    }
}