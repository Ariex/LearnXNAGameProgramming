using Microsoft.Xna.Framework.Graphics;
using System;
namespace LearnXNAGame.Maps
{
	public interface IMapCell
	{
		IMapCellNeighbor<IMapCell> Neighbors { get; }
		float X { get; set; }
		float Y { get; set; }
		void Draw(SpriteBatch sb, float offsetX = 0f, float offsetY = 0f);

		bool IsHover { get; set; }
	}
}
