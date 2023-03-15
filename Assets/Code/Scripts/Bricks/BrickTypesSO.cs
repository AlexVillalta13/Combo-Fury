using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Brick Types")]
public class BrickTypesSO : ScriptableObject
{
    [SerializeField] List<BrickTypes> brickTypes = new List<BrickTypes>();
    public List<BrickTypes> BrickTypes { get { return brickTypes; } }
}
[System.Serializable]
public class BrickTypes
{
    [SerializeField] BrickTypeEnum brickType;
    public BrickTypeEnum BrickType { get { return brickType; } }
    [SerializeField] VisualTreeAsset brickUIAsset;
    public VisualTreeAsset BrickUIAsset { get { return brickUIAsset; } }
    [SerializeField] float timeToAutoDelete;
    public float TimeToAutoDelete { get { return timeToAutoDelete; } }
    [SerializeField] float minWidth;
    public float MinWidth { get { return minWidth; } }
    [SerializeField] float maxWidth;
    public float MaxWidth { get { return maxWidth; } }
}