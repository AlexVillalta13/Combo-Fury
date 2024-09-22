public interface ILevelData
{
    public SceneEnum GetEnvironment();
    
    public EnemyData GetEnemy(int enemyNumber);
    public int GetTotalEnemiesCount();
}