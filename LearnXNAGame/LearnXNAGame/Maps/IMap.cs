using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnXNAGame.Maps
{
	public interface IMap
	{
		int CellWidth { get; }
		void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb);
		int Height { get; }
		int Width { get; }

		void Drag(float x, float y);
		void Release(float x, float y);

		void UpdateMousePos(float x, float y);
	}
}
