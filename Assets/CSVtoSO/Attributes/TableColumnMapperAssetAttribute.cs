namespace CSVtoSO.Attributes {

    using System;
    using System.Reflection;
    using CSVtoSO.Utilities;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Tag to be applied to asset-type fields like <see cref="GameObject"/>s or <see cref="Sprite"/>s.<br/><br/>
    /// It is mandatory to specify the <b>Column Name</b> as well as the <b>Asset's Path</b> when applying the Tag.<br/><br/>
    /// </summary>
    public class TableColumnMapperAssetAttribute : TableColumnMapperAttribute {
        public string assetPath;
        public string fileExtension;
        public bool addTypePathToAssetPath;

        /// <summary>
        /// Construct the attribute providing a column name and the asset path (i.e. Assets/MyObjects/...).
        /// </summary>
        /// <param name="columnName">The mandatory name of the column the asset belongs to in the csv database.</param>
        /// <param name="assetPath">The mandatory folder's path that the matching asset is in.</param>
        /// <param name="addTypePathToAssetPath">Set this to <see cref="true"/> to add the Type's path of the object using this attribute to the corresponding asset's path.</param>
        /// <param name="fileExtension">If not provided in the corresponding database's record, add the file's extension in order to find a match.</param>
        public TableColumnMapperAssetAttribute (string columnName, string assetPath, bool addTypePathToAssetPath = false, string fileExtension = null) : base(columnName) {
            this.assetPath = assetPath;
            if(!this.assetPath.EndsWith("/", StringComparison.InvariantCultureIgnoreCase))
                this.assetPath += "/";

            this.fileExtension = fileExtension;
            this.addTypePathToAssetPath = addTypePathToAssetPath;
        }

#if UNITY_EDITOR

        /// <summary>
        /// Loads the value of the specific instance's asset field leaning on the utility field loader.
        /// </summary>
        /// <param name="instance">The object that is target of the field's loading.</param>
        /// <param name="fieldInfo">The representation of the specific asset field that has to be loaded.</param>
        /// <param name="loader">The utility that provides the actual connection to the field's loading.</param>
        /// <param name="rowIndex">The index of the table's row that this object should represent.</param>
        /// <param name="postFix">The eventual postfix to put at the end of a column's name (might be useful for internal styling and conventions).</param>
        public override void LoadField (ref object instance, FieldInfo fieldInfo, TableLoaderUtility loader, int rowIndex, string postFix) {
            try {
                if(fieldInfo.FieldType.IsArray) {
                    string[] recordValues = loader.GetStringAt(columnName, rowIndex, postFix).Split(new string[] { ";"}, StringSplitOptions.RemoveEmptyEntries);

                    Type arrayType = fieldInfo.FieldType;
                    Type fieldType = arrayType.GetElementType();

                    Array arrayInstance = Array.CreateInstance(fieldType, recordValues.Length);

                    for(int i = 0; i < recordValues.Length; i++) {
                        if(string.IsNullOrEmpty(recordValues[i])) {
                            Debug.LogWarningFormat("Null or empty value for table <b>{0}</b>, at column <b>{1}</b> - row <b>{2}</b> at position {3} of the array.", loader.TableTitle, columnName, rowIndex, i);
                            continue;
                        }

                        string fullPath = assetPath;

                        if(addTypePathToAssetPath)
                            fullPath += string.Format("{0}/", instance.GetType().ToString());

                        fullPath += recordValues[i];

                        if(!string.IsNullOrEmpty(fileExtension))
                            fullPath += fileExtension;

                        object asset = AssetDatabase.LoadAssetAtPath(fullPath, fieldType);

                        if(asset == null)
                            Debug.LogErrorFormat("Unable to load asset at path <b>{0}</b> for the table <b>{1}</b> at column <b>{2}</b>, row <b>{3}</b>.\nObject Type: <b>{4}</b>, field <b>{5}</b>",
                                fullPath, loader.TableTitle, columnName, rowIndex + 2, instance.GetType().ToString(), fieldInfo.Name);

                        arrayInstance.SetValue(asset, i);
                    }

                    fieldInfo.SetValue(instance, arrayInstance);
                }
                else {
                    Type fieldType = fieldInfo.FieldType;

                    string recordValue = loader.GetStringAt(columnName, rowIndex, postFix);

                    if(string.IsNullOrEmpty(recordValue)) {
                        Debug.LogWarningFormat("Value for record at column <b>{0}</b>, row <b>{1}</b> in table <b>{2}</b> is either null or empty, cannot load asset.", columnName, rowIndex, loader.TableTitle);
                        return;
                    }

                    string fullPath = assetPath;
                    if(addTypePathToAssetPath)
                        fullPath += string.Format("{0}/", instance.GetType().ToString());

                    fullPath += recordValue;

                    if(!string.IsNullOrEmpty(fileExtension))
                        fullPath += fileExtension;

                    object asset = AssetDatabase.LoadAssetAtPath(fullPath, fieldType);

                    if(asset == null)
                        Debug.LogErrorFormat("Unable to load asset at path <b>{0}</b> for the table <b>{1}</b> at column <b>{2}</b>, row <b>{3}</b>.\nObject Type: <b>{4}</b>, field <b>{5}</b>",
                            fullPath, loader.TableTitle, columnName, rowIndex + 2, instance.GetType().ToString(), fieldInfo.Name);

                    fieldInfo.SetValue(instance, asset);
                }
            }
            catch(Exception e) {
                Debug.LogErrorFormat("Cannot process asset for table <b>{0}<b/> at column <b>{1}<b/> - row <b>{2}<b/>.\nThrown: {3}", loader.TableTitle, columnName, rowIndex, e.ToString());
            }
        }

#endif
    }
}