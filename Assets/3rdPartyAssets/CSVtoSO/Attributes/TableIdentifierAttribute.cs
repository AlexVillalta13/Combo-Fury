namespace CSVtoSO.Attributes {

    using System;

    /// <summary>
    /// Apply this Tag to the classes implementing the actual database's structure.<br/><br/>
    /// The <b>Table's Name</b> must match the actual name of the corresponding .csv file.
    /// </summary>

    [AttributeUsage(AttributeTargets.Class)]
    public class TableIdentifierAttribute : Attribute {
        public string tableName;

        /// <summary>
        /// Construct the Tag to put on a database's belonging class specifying what is the name of the corresponding csv file to inspect.
        /// </summary>
        /// <param name="tableFileName">The exact name of the csv file that should match for the class.</param>
        public TableIdentifierAttribute (string tableFileName) {
            tableName = tableFileName;
        }
    }
}