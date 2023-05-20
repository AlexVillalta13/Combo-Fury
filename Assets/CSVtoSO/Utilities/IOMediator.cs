namespace CSVtoSO.Utilities {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Base class used to start the file reading and conversion.
    /// </summary>
    public class IOMediator {

        /// <summary>
        /// Given the .csv file, extracts all the rows and columns in a record matrix.
        /// </summary>
        /// <param name="csvTable">The actual .csv file.</param>
        /// <returns>A matrix containing the representation of the .csv file.</returns>
        public static IList<IList<object>> GetTableData (UnityEngine.Object csvTable) {
            string tablePath = AssetDatabase.GetAssetPath(csvTable);

            IList<IList<object>> objectTable = new List<IList<object>>();

            try {
                string[] tableRows = File.ReadAllLines(tablePath);

                foreach(var line in tableRows) {
                    string[] splitData = line.Split(',');

                    var objects = (object[])splitData;

                    objectTable.Add(objects.ToList());
                }
#pragma warning disable IDE0004
                return (IList<IList<object>>)objectTable;
#pragma warning restore IDE0004
            }
            catch(Exception e) {
                Debug.LogErrorFormat("Error while reading one of the .csv files.\nThrown {0}", e.ToString());
                return null;
            }
        }
    }
}