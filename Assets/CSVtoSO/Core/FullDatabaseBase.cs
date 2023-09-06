namespace CSVtoSO.Core {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CSVtoSO.Attributes;
    using CSVtoSO.Utilities;
    using UnityEngine;

    /// <summary>
    /// Base class to inherit from that should include arrays of specific implementations of <see cref="TableObject"/>s.<br/><br/>
    /// </summary>
    public class FullDatabaseBase : ScriptableObject {

        /// <summary>
        /// The set of .csv files to read and load into the ScriptableObject's asset database.<br/><br/>
        /// <b>NOTE:</b> in order to load the tables, each .csv file's name must match with the <see cref="TableIdentifierAttribute"/> tag's table name parameter.
        /// </summary>
        [SerializeField] public List<UnityEngine.Object> tables = new List<UnityEngine.Object>();

#if UNITY_EDITOR

        public virtual void ExportData() { }

        /// <summary>
        /// Called via button on the inspector of the database's asset to fill it according to the list of .csv files provided.
        /// </summary>
        /// <param name="tableReaders"></param>
        public void LoadFromDatabase(List<TableDataReaderUtility> tableReaders)
        {
            try
            {
                BindingFlags filter = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;

                FieldInfo[] fields = GetType().GetFields(filter);

                System.Diagnostics.Stopwatch stopWatch = new();

                foreach (var field in fields)
                {
                    stopWatch.Start();

                    if (field.FieldType.IsArray)
                    {
                        Type arrayType = field.FieldType;
                        Type fieldType = arrayType.GetElementType();

                        if (!fieldType.IsSubclassOf(typeof(TableObject)))
                        {
                            Debug.LogWarningFormat("Class <b>{0}</b> does not inherit from <b>TableObject</b>, fill its values manually", fieldType);
                            continue;
                        }

                        var tableAttribute = (TableIdentifierAttribute)Attribute.GetCustomAttribute(fieldType, typeof(TableIdentifierAttribute));

                        if (tableAttribute != null)
                        {
                            int tableTotalRows = 0;

                            List<TableLoaderUtility> tableLoaders = new();

                            var tableDataRW = tableReaders.FirstOrDefault(rw => rw.TableTitle.Equals(tableAttribute.tableName, StringComparison.InvariantCultureIgnoreCase));

                            if (tableDataRW != null)
                            {
                                TableLoaderUtility loader = new TableLoaderUtility(tableDataRW);
                                tableTotalRows += loader.TableLenght;
                                tableLoaders.Add(loader);
                            }
                            else
                                Debug.LogErrorFormat("Table <b>{0}</b> for target class <b>{1}</b> not found. Either provide one or check corresponding file name", tableAttribute.tableName, fieldType);

                            Array arrayInstance = Array.CreateInstance(fieldType, tableTotalRows);
                            int index = 0;

                            foreach (var loader in tableLoaders)
                            {
                                for (int i = 0; i < loader.TableLenght; i++)
                                {
                                    try
                                    {
                                        TableObject tableObject = (TableObject)Activator.CreateInstance(fieldType);
                                        tableObject.ReadRow(loader, i, "");
                                        arrayInstance.SetValue(tableObject, index);
                                        index++;
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.LogErrorFormat("Cannot import table <b>{0}</b> attached to <b>{1}</b> class at row {2}.\nThrown {3}", loader.TableTitle, fieldType, i + 2, e.ToString());
                                    }
                                }
                            }
                            field.SetValue(this, arrayInstance);
                        }
                    }
                    else
                    {
                        Type fieldType = field.FieldType;

                        if (!fieldType.IsSubclassOf(typeof(TableObject)))
                        {
                            Debug.LogWarningFormat("Class <b>{0}</b> has <b>{1}</b> member that does not inherit from <b>TableObject</b>, it won't be imported.", fieldType, field.Name);
                            continue;
                        }

                        var tableAttribute = (TableIdentifierAttribute)Attribute.GetCustomAttribute(fieldType, typeof(TableIdentifierAttribute));

                        if (tableAttribute != null)
                        {
                            int tableTotalRows = 0;

                            List<TableLoaderUtility> tableLoaders = new List<TableLoaderUtility>();

                            var tableData = tableReaders.FirstOrDefault(rw => rw.TableTitle.Equals(tableAttribute.tableName, StringComparison.InvariantCultureIgnoreCase));

                            if (tableData != null)
                            {
                                TableLoaderUtility loader = new TableLoaderUtility(tableData);
                                tableTotalRows += loader.TableLenght;
                                tableLoaders.Add(loader);
                            }
                            else
                                Debug.LogErrorFormat("Table <b>{0}</b> for target class <b>{1}</b> not found", tableAttribute.tableName, fieldType);

                            foreach (var loader in tableLoaders)
                            {
                                try
                                {
                                    for (int i = 0; i < loader.TableLenght; i++)
                                    {
                                        TableObject tableObject = (TableObject)Activator.CreateInstance(fieldType);
                                        tableObject.ReadRow(loader, i, "");
                                        field.SetValue(this, tableObject);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Debug.LogErrorFormat("Cannot import table <b>{0}</b> attached to <b>{1}</b> class.\nThrown {2}", loader.TableTitle, fieldType, e.ToString());
                                }
                            }
                        }
                    }

                    stopWatch.Stop();
                    Debug.LogFormat("Table loaded in: {0}ms", stopWatch.ElapsedMilliseconds);
                    stopWatch.Reset();
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Critical error while importing.\nThrown {0}", e.ToString());
            }
        }

#endif
    }
}