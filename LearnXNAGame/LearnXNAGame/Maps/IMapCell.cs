using Microsoft.Xna.Framework.Graphics;
using System;
namespace LearnXNAGame.Maps
{
	public interface IMapCell
	{
		IMapCellNeighbor<IMapCell> Neighbors { get; }
		int X { get; set; }
		int Y { get; set; }
		void Draw(SpriteBatch sb, float offsetX = 0f, float offsetY = 0f);

		bool IsHover { get; set; }
	}
}
