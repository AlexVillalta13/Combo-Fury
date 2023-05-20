namespace CSVtoSO.Tests {

    using CSVtoSO.Attributes;
    using CSVtoSO.Core;
    using UnityEngine;

    [System.Serializable]
    [TableIdentifier("Array Values Table")]
    public class TestArrayValuesTableDatabase : TableObject {
        [TableColumnMapper("testStringArray")] public string[] testStringArray;

        [TableColumnMapper("testCharArray")] public char[] testCharArray;

        [TableColumnMapper("testSbyteArray")] public sbyte[] testSbyteArray;

        [TableColumnMapper("testByteArray")] public byte[] testByteArray;

        [TableColumnMapper("testShortArray")] public short[] testShortArray;

        [TableColumnMapper("testUshortArray")] public ushort[] testUshortArray;

        [TableColumnMapper("testIntArray")] public int[] testIntArray;

        [TableColumnMapper("testUintArray")] public uint[] testUintArray;

        [TableColumnMapper("testLongArray")] public long[] testLongArray;

        [TableColumnMapper("testUlongArray")] public ulong[] testUlongArray;

        [TableColumnMapper("testFloatArray")] public float[] testFloatArray;

        [TableColumnMapper("testDoubleArray")] public double[] testDoubleArray;

        [TableColumnMapper("testDecimalArray")] public decimal[] testDecimalArray;

        [TableColumnMapper("testBoolArray")] public bool[] testBoolArray;

        [TableColumnMapper("testEnumArray")] public TestEnum[] testEnumArray;

        [TableColumnMapper("testColorArray")] public Color[] testColorArray;

        [TableColumnMapperAsset("testGameobjectsArray", "Assets/Scripts/SimpleCSVReader/Tests", fileExtension: ".prefab")] public GameObject[] testGameobjectsArray;
    }
}