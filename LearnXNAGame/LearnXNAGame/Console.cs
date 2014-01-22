using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnXNAGame
{
	public class Console
	{
		private SpriteBatch sb;
		private SpriteFont font;
		private List<Tuple<string, int>> logs = new List<Tuple<string, int>>();

		public Console(SpriteBatch sb, SpriteFont font)
		{
			this.sb = sb;
			this.font = font;
		}

		public void Log(string msg, int line = -1)
		{
			this.logs.Add(new Tuple<string, int>(msg, line == -1 ? this.logs.Count : line));
		}

		public void Draw()
		{
			var copy = this.logs;
			this.logs = new List<Tuple<string, int>>();
			copy.ForEach(l =>
			{
				this.sb.DrawString(this.font, l.Item1, new Microsoft.Xna.Framework.Vector2(0, l.Item2 * 14), Microsoft.Xna.Framework.Color.Black);
			});
			copy.Clear();
		}
	}
}
