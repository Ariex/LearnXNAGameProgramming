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

		//protected float offsetX = 0;
		//protected float offsetY = 0;
		protected Microsoft.Xna.Framework.Vector2 MapOffset = new Microsoft.Xna.Framework.Vector2(0, 0);

		public HexagonMap()
			: this(4, 4, 40)
		{

		}

		public HexagonMap(int width, int height, int cellWidth)
		{
			this.Width = width;
			this.Height = height;
			this.CellWidth = cellWidth;
			this.MapOffset = new Microsoft.Xna.Framework.Vector2(this.CellWidth, this.CellWidth * 0.866f);
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
		private Microsoft.Xna.Framework.Vector2 oldOffset = new Microsoft.Xna.Framework.Vector2(0, 0);
		public void Drag(float x, float y)
		{
			//this.Console.Log("Drag Start: " + this.dragStart.X + ":" + this.dragStart.Y);
			if (!isDragging)
			{
				// drag starts
				this.dragStart = new Microsoft.Xna.Framework.Vector2(x, y);
				this.oldOffset = new Microsoft.Xna.Framework.Vector2(this.MapOffset.X, this.MapOffset.Y);
				isDragging = true;
			}
			else
			{
				// dragging
				this.MapOffset.X = this.oldOffset.X + x - this.dragStart.X;
				this.MapOffset.Y = this.oldOffset.Y + y - this.dragStart.Y;
			}
		}

		public void Release(float x, float y)
		{
			this.isDragging = false;
			//this.offsetX = this.oldOffset.X;
			//this.offsetY = this.oldOffset.Y;
		}

		public void UpdateMousePos(float x, float y)
		{
			//this.Console.Log("X:" + x + " Y:" + y, 0);
			//this.Console.Log("OffsetX: " + this.offsetX);
			//this.Console.Log("OffsetY: " + this.offsetY);

			var cell = this.FindCellFromPosition(x, y);
			if (cell != null)
			{
				cell.IsHover = true;
			}
		}

		private IMapCell FindCellFromPosition(float x, float y)
		{
			var tmpC = this.CellWidth * 0.866f;
			var rx = x - this.MapOffset.X;
			var ry = y - this.MapOffset.Y;

			// this.Console.Log("rx:" + rx + " ry:" + ry, 1);

			var row = ry / tmpC / 2;
			var irow = (int)Math.Ceiling(row);
			var col = rx / this.CellWidth / 0.75f / 2;
			var icol = (int)Math.Ceiling(col);
			if (irow < 0 || irow > this.Height || icol < 0 || icol > this.Width)
			{
				return null;
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
				return closest;
			}
			return null;
		}

		public void Draw(SpriteBatch sb)
		{
			var topLeftCell = this.FindCellFromPosition(0, 0) ?? this.MapCells[0, 0];
			var bottomRightCell = this.FindCellFromPosition(1024, 768) ?? this.MapCells[this.Width - 1, this.Height - 1];
			for (var i = topLeftCell.X; i <= bottomRightCell.X; i++)
			{
				for (var j = topLeftCell.Y; j <= bottomRightCell.Y; j++)
				{
					this.MapCells[i, j].Draw(sb, this.MapOffset.X, this.MapOffset.Y);
				}
			}
		}
	}
}
