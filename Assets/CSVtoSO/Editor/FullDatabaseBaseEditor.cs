namespace CSVtoSO.Editor {

    using System;
    using System.Collections.Generic;
    using CSVtoSO.Core;
    using CSVtoSO.Utilities;
    using UnityEditor;
    using UnityEngine;
    using System.Linq;
    using System.IO;

    [CustomEditor(typeof(FullDatabaseBase), editorForChildClasses: true)]
    public class FullDatabaseBaseEditor : Editor {
        public override void OnInspectorGUI () {
            DrawDefaultInspector();

            GUILayout.Space(10);

            if((target as FullDatabaseBase).tables.Count == 0 || (target as FullDatabaseBase).tables.Where(t => t == null).ToList().Count != 0) {
                EditorGUILayout.HelpBox("Unable to load with no tables or empty table fields", MessageType.Error);
            }
            if((target as FullDatabaseBase).tables.Any(t => !Path.GetExtension(AssetDatabase.GetAssetPath(t)).Equals(".csv"))) {
                EditorGUILayout.HelpBox("One or more objects is not a .csv file, either check file extension or remove wrong assets.", MessageType.Error);
            }
            else if(Application.isPlaying) {
                EditorGUILayout.HelpBox("Unable to load during play mode", MessageType.Error);
            }
            else {
                if(GUILayout.Button("Load") && EditorUtility.DisplayDialog("Load", "Confirm loading data?", "Yes", "Cancel")) {
                    Debug.Log("Loading...");

                    LoadData();
                }
            }
        }

        protected virtual void LoadData () {
            try {
                List<TableDataReaderUtility> tableReaders = GetAllData();

                if(tableReaders.Count == 0)
                    throw new Exception("Database is empty");

                var databaseObject = target as FullDatabaseBase;
                databaseObject.LoadFromDatabase(tableReaders);
                EditorUtility.SetDirty(databaseObject);
            }
            catch(Exception e) {
                Debug.LogException(e);
            }
        }

        private List<TableDataReaderUtility> GetAllData () {

            var database = target as FullDatabaseBase;

            List<TableDataReaderUtility> readers = new();

            if(database.tables == null)
                throw new Exception("Tables parameter cannot be null.");
            if(database.tables.Count == 0)
                throw new Exception("Tables parameter has no elements.");

            for(int i = 0; i < database.tables.Count; i++) {
                TableDataReaderUtility reader = new(database.tables[i].name, IOMediator.GetTableData(database.tables[i]));

                readers.Add(reader);
            }

            return readers;
        }
    }
}
