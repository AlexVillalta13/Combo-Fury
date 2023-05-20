namespace CSVtoSO.Tests {

    using CSVtoSO.Attributes;
    using CSVtoSO.Core;
    using UnityEngine;

    [System.Serializable]
    //[TableIdentifier("Levels design")]

    [TableIdentifier("Single Values Table")]
    public class TestSingleValuesTableDatabase : TableObject {
        [TableColumnMapper("Max Health")] public int maxHealth;


        [TableColumnMapper("testString")] public string testString;

        [TableColumnMapper("testChar")] public char testChar;

        [TableColumnMapper("testSbyte")] public sbyte testSbyte;

        [TableColumnMapper("testByte")] public byte testByte;

        [TableColumnMapper("testShort")] public short testShort;

        [TableColumnMapper("testUshort")] public ushort testUshort;

        [TableColumnMapper("testInt")] public int testInt;

        [TableColumnMapper("testUint")] public uint testUint;

        [TableColumnMapper("testLong")] public long testLong;

        [TableColumnMapper("testUlong")] public ulong testUlong;

        [TableColumnMapper("testFloat")] public float testFloat;

        [TableColumnMapper("testDouble")] public double testDouble;

        [TableColumnMapper("testDecimal")] public decimal testDecimal;
        [TableColumnMapper("testBool")] public bool testBool;

        [TableColumnMapper("testEnum")] public TestEnum testEnum;

        [TableColumnMapper("testColor")] public Color testColor;
    }
}