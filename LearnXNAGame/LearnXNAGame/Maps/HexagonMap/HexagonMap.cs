using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C3.XNA;

namespace LearnXNAGame.Maps.HexagonMap
{
	public class HexagonMap : IMap, ILogable
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public int CellWidth { get; private set; }

		public Console Console { get; set; }

		protected HexagonMapCell[,] MapCells;

		protected float offsetX = 0;
		protected float offsetY = 0;

		public HexagonMap()
			: this(4, 4, 40)
		{

		}

		public HexagonMap(int width, int height, int cellWidth)
		{
			this.Width = width;
			this.Height = height;
			this.CellWidth = cellWidth;
			this.offsetX = this.CellWidth;
			this.offsetY = this.CellWidth * 0.866f;
			this.MapCells = new HexagonMapCell[width, height];
			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					this.MapCells[i, j] = new HexagonMapCell(i, j, cellWidth);
				}
			}
		}

		private Microsoft.Xna.Framework.Vector2 dragStart = new Microsoft.Xna.Framework.Vector2(0, 0);
		private bool isDragging = false;
		private Microsoft.Xna.Framework.Vector2 lastPoint = new Microsoft.Xna.Framework.Vector2(0, 0);
		public void Drag(float x, float y)
		{
			if (!isDragging)
			{
				// drag starts
				this.dragStart.X = x;
				this.dragStart.Y = y;
				isDragging = true;
			}
			else
			{
				// dragging
				this.offsetX = x - this.lastPoint.X;
				this.offsetY = y - this.lastPoint.Y;
				this.lastPoint.X = x;
				this.lastPoint.Y = y;
			}
		}

		public void Release(float x, float y)
		{
			this.isDragging = false;
		}

		public void UpdateMousePos(float x, float y)
		{
			this.Console.Log("X:" + x + " Y:" + y, 0);
			this.Console.Log("OffsetX: " + this.offsetX);
			this.Console.Log("OffsetY: " + this.offsetY);

			var tmpC = this.CellWidth * 0.866f;
			var rx = x - this.offsetX;
			var ry = y - this.offsetY;

			// this.Console.Log("rx:" + rx + " ry:" + ry, 1);

			var row = ry / tmpC / 2;
			var irow = (int)Math.Ceiling(row);
			var col = rx / this.CellWidth / 0.75f / 2;
			var icol = (int)Math.Ceiling(col);
			if (irow < 0 || irow > this.Height || icol < 0 || icol > this.Width)
			{
				return;
			}
			// this.Console.Log("row:" + row + " col:" + icol, 2);
			var suspects = new List<HexagonMapCell>(6);
			var setSuspects = new Action<int, int>((i, j) =>
			{
				if (i > -1 && i < this.Width && j > -1 && j < this.Height)
				{
					suspects.Add(this.MapCells[i, j]);
				}
			});
			var setHover = new Action<int, int>((i, j) =>
			{
				if (i > -1 && i < this.Width && j > -1 && j < this.Height)
				{
					this.MapCells[i, j].IsHover = true;
				}
			});
			setSuspects(icol, irow);
			setSuspects(icol - 1, irow);
			setSuspects(icol, irow - 1);
			setSuspects(icol + 1, irow);
			setSuspects(icol, irow + 1);
			setSuspects(icol - 1, irow - 1);
			setSuspects(icol + 1, irow - 1);

			HexagonMapCell closest = null;
			var shortest = float.MaxValue;
			suspects.ForEach(s =>
			{
				var d = (s.PhysicalX - rx) * (s.PhysicalX - rx) + (s.PhysicalY - ry) * (s.PhysicalY - ry);
				if (d < shortest)
				{
					shortest = d;
					closest = s;
				}
			});
			if (closest != null && shortest <= this.CellWidth * this.CellWidth)
			{
				closest.IsHover = true;
			}
		}

		public void Draw(SpriteBatch sb)
		{

			for (var i = 0; i < this.Width; i++)
			{
				for (var j = 0; j < this.Height; j++)
				{
					this.MapCells[i, j].Draw(sb, this.offsetX, this.offsetY);
				}
			}
		}
	}
}
