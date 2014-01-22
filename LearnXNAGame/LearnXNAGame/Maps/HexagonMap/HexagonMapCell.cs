using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3.XNA;

namespace LearnXNAGame.Maps.HexagonMap
{
	public class HexagonMapCell : IMapCell
	{
		public int X { get; set; }
		public int Y { get; set; }
		public float R { get; set; }
		public bool IsHover { get; set; }
		public float PhysicalX { get; private set; }
		public float PhysicalY { get; private set; }

		public HexagonNeighbor Neighbors
		{
			get;
			protected set;
		}

		IMapCellNeighbor<IMapCell> IMapCell.Neighbors
		{
			get { return (IMapCellNeighbor<IMapCell>)this.Neighbors; }
		}

		public HexagonMapCell(int x, int y, float r)
		{
			this.Neighbors = new HexagonNeighbor();
			this.X = x;
			this.Y = y;
			this.R = r;
			this.PhysicalX = this.X *this.R * 1.5f;
			this.PhysicalY = this.Y *this.R * 1.732f - (this.X % 2 == 0 ? 0 : -this.R * 0.866f);
		}

		public void Draw(SpriteBatch sb, float offsetX = 0f, float offsetY = 0f)
		{
			// var q3r = this.R * 0.866f;
			// var p1 = new Point<float, float>(this.X - this.R, this.Y);
			sb.DrawCircle(
				offsetX + this.PhysicalX,// this.X * this.R * 1.5f,
				offsetY + this.PhysicalY,//this.Y * this.R * 1.732f - (this.X % 2 == 0 ? 0 : -q3r),
				this.R,
				6,
				this.IsHover ? Microsoft.Xna.Framework.Color.Red : Microsoft.Xna.Framework.Color.Black, 1f
			);
			this.IsHover = false;
		}
	}
}
