namespace CSVtoSO.Tests {

    using CSVtoSO.Core;
    using UnityEngine;

    [CreateAssetMenu(menuName = "CSVReader/Test/New Test Database", fileName = "New Test Database")]
    public class TestFullDatabase : FullDatabaseBase {
        [NonReorderable] public TestSingleValuesTableDatabase[] singleValuesDatabase;

        [NonReorderable] public TestArrayValuesTableDatabase[] arrayValuesDatabase;
    }

    public enum TestEnum {
        test1,
        test2,
        test3
    }
}