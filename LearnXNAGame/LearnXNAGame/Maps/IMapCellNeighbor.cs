using System;
namespace LearnXNAGame.Maps
{
	public interface IMapCellNeighbor<TMapCell>
	 where TMapCell : IMapCell
	{
		TMapCell this[int i] { get; set; }
		int TotalAvailableNeighbors { get; }
	}

	public class MapCellNeighbor<TMapCell> : IMapCellNeighbor<TMapCell> where TMapCell : IMapCell
	{
		protected TMapCell[] cells { get; set; }

		public int TotalAvailableNeighbors { get; private set; }

		public virtual TMapCell this[int i]
		{
			get
			{
				return this.cells[i % this.TotalAvailableNeighbors];
			}
			set
			{
				this.cells[i % this.TotalAvailableNeighbors] = value;
			}
		}

		public MapCellNeighbor(int totalNeighbors)
		{
			this.TotalAvailableNeighbors = totalNeighbors;
		}
	}
}
