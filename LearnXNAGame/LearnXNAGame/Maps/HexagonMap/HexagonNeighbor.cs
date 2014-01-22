using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnXNAGame.Maps.HexagonMap
{
	public class HexagonNeighbor : MapCellNeighbor<HexagonMapCell>
	{
		public HexagonNeighbor()
			: base(6)
		{
		}
	}
}
