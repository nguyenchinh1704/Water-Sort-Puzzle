using System;

public class TubeModel
{
	public int id;
	public int[] Color;
	public int maxColor;
	public TubeModel(int id, int maxColor)
	{
		this.id = id;
		this.maxColor = maxColor;
		Color = new int[maxColor];
	}
	public void RandomColor(int colorNum)
	{
		Color = new int[maxColor];

		for (int i = 0; i < Color.Length; i++)
		{
			Color[i] = UnityEngine.Random.Range(0, colorNum);
		}
	}
}
