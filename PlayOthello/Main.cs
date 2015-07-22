	/*
	*
	*	JENSON JOSEPH
	*	ARTIFICIAL INTELLIGENCE 
	*	ORTHELLO PROGRAM
	*
	*/
	

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayOthello
{
    public partial class frmMain : Form
    {
        private class GridMove:IComparable<GridMove>
        {
            public int Quality = 0;
            public int PosX = 0;
            public int PoxY = 0;
            public List<int> Moves;

            public int CompareTo( GridMove g )
            {
                return g.Quality - Quality;
            }
        }

        public enum GameplayState { gs_default, gs_playing, gs_turnwait, gs_gameend };
        public enum GameTile { gt_blank, gt_black, gt_white }

        private PictureBox[] Boxes = null;
        private GameTile[] Grid = null;

        private GameplayState GameState = GameplayState.gs_default;
        private GameTile CurrentPlay;

        private static int OrthelloSize = 8;

        public frmMain()
        {
            InitializeComponent();

            BuildGameBoxes( this.flpPlayItems );
        }

        private Image LoadTile(GameTile TileType)
        {
            string str = "orthello-0";
            if (TileType == GameTile.gt_black) str += "2";
            else if (TileType == GameTile.gt_white) str += "3";
            str += ".png";

            return new Bitmap( str );
        }

        public void SetTile( int x, int y, GameTile Tile )
        {
            Boxes[IndexPosition(x, y)].Image = LoadTile(Tile);
            Grid[IndexPosition(x, y)] = Tile;
        }

        public void BuildGameBoxes(FlowLayoutPanel Layout)
        {
            float CWidth = (Layout.Width) / (OrthelloSize + 1),
                CHeight = (Layout.Height) / (OrthelloSize+1);

            Boxes = new PictureBox[ (OrthelloSize * OrthelloSize) ];
            Grid = new GameTile[ Boxes.Length ];

            for (int i = 0; i < OrthelloSize; i++)
            {
                for (int j = 0; j < OrthelloSize; j++)
                {
                    PictureBox NewTile = new PictureBox();

                    NewTile.Width = NewTile.Height = (int)Math.Min( CWidth, CHeight );
                    NewTile.BackColor = SystemColors.Control;
                    NewTile.SizeMode = PictureBoxSizeMode.StretchImage;
                    NewTile.Tag = IndexPosition(j, i);
                    NewTile.Click += clickTile;

                    Label Loc = new Label();
                    Loc.Text = (i + 1).ToString() + ", " + (j + 1).ToString();
                    Loc.BackColor = Color.Transparent;
                    NewTile.Controls.Add(Loc);

                    Layout.Controls.Add( NewTile );
                    Boxes[(i*OrthelloSize)+j] = NewTile;
                }
            }
        }

        public void clickTile(Object Sender, EventArgs e)
        {
            int tag = (int)((Control)(Sender)).Tag;

            tbxPosX.Text = ((tag % OrthelloSize)+1).ToString();
            tbxPosY.Text = ((tag / OrthelloSize)+1).ToString();
        }

        public int IndexPosition(int x, int y)
        {
            return (y * OrthelloSize) + x;
        }

        public void InitializeGame( )
        {
            int MidBoxX = (OrthelloSize / 2)-1;
            int MidBoxY = MidBoxX;

            SetTile(MidBoxX, MidBoxY, GameTile.gt_white);
            SetTile(MidBoxX+1, MidBoxY, GameTile.gt_black);
            SetTile(MidBoxX, MidBoxY+1, GameTile.gt_black);
            SetTile(MidBoxX+1, MidBoxY+1, GameTile.gt_white);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            switch (this.GameState)
            {
                case GameplayState.gs_default:
                case GameplayState.gs_gameend:
                    {
                        DialogResult res = MessageBox.Show("Would you like to play first?",
                            "Play First",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        InitializeGame();

                        if (res == DialogResult.Yes)
                        {
                            GameState = GameplayState.gs_turnwait;
                            btnPlay.Text = "Waiting on Input";
                            CurrentPlay = GameTile.gt_black;
                        }
                        else
                        {
                            GameState = GameplayState.gs_playing;
                            btnPlay.Text = "AI Play...";
                            CurrentPlay = GameTile.gt_white;
                        }

                        picCurrent.Image = LoadTile(CurrentPlay);
                    } break;
                case GameplayState.gs_turnwait:
                    {
                        int x = 0, y = 0;
                        try
                        {
                            x = Int32.Parse(tbxPosX.Text) - 1;
                            y = Int32.Parse(tbxPosY.Text) - 1;
                        }
                        catch (Exception)
                        {
                        }

                        List<int> Plays = FindPairsThatSurround(x, y, CurrentPlay);

                        if (Plays.Count == 0 
                            || Grid[ IndexPosition( x, y )] != GameTile.gt_blank )
                        {
                            lblDisplay.Text = "Invalid Play";

                            tbxPosX.Clear();
                            tbxPosY.Clear();
                        }
                        else
                        {
                            TurnTiles(x, y, Plays, this.CurrentPlay);
                            CurrentPlay = (CurrentPlay == GameTile.gt_black) ? GameTile.gt_white : GameTile.gt_black;

                            tbxPosX.Clear();
                            tbxPosY.Clear();

                            btnPlay.Text = "AI Play...";

                            GameState = GameplayState.gs_playing;
                        }
                    } break;
                case GameplayState.gs_playing:
                    {
                        List<GridMove> Moves = GenerateMoves(CurrentPlay);

                        if (Moves.Count == 0)
                        {
                            lblDisplay.Text = "NO MOVES, PASS";
                        }
                        else
                        {
                            GridMove cMove = Moves.First();

                            TurnTiles(cMove.PosX, cMove.PoxY, cMove.Moves, this.CurrentPlay);
                            CurrentPlay = (CurrentPlay == GameTile.gt_black) ? GameTile.gt_white : GameTile.gt_black;

                            lblDisplay.Text = "Move Tile: Y:" + (cMove.PoxY+1).ToString() + ", X:" + (cMove.PosX+1).ToString(); 
                        }
                        GameState = GameplayState.gs_turnwait;

                        btnPlay.Text = "Waiting on Input";
                    } break;
            }

            picCurrent.Image = LoadTile(CurrentPlay);
            doScore();
        }

        private int verifyTile(GameTile Base, GameTile CTile, int Range)
        {
            if (CTile == GameTile.gt_blank) return 0;
            else if (CTile == Base && Math.Abs(Range) > 1) return 1;

            return 2;
        }


        private void doScore()
        {
            int WhiteScore = 0, BlackScore = 0;

            foreach (GameTile Tile in Grid)
                if (Tile == GameTile.gt_black) BlackScore++;
                else if (Tile == GameTile.gt_white) WhiteScore++;

            lblBlackScore.Text = "Black Score: " + BlackScore.ToString();
            lblWhiteScore.Text = "White Score: " + WhiteScore.ToString();

            if (WhiteScore + BlackScore == OrthelloSize * OrthelloSize)
            {
                GameState = GameplayState.gs_gameend;
                btnPlay.Text = "Start";

                MessageBox.Show( "Game End: " + ((WhiteScore>BlackScore)?"White":"Black") + " wins.\n\n Game will reset.",
                    "Game End",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information );
            }
        }

        //  Find locations of items that will turn the players tiles

        private List<int> FindPairsThatSurround(int x, int y, GameTile Tile)
        {
            List<int> Result = new List<int>();

            Boolean complete = false;
            int discovered = 0;

            // Rgiht

            for (int i = x + 1; i < OrthelloSize && !complete; i++)
            {
                GameTile CTile = Grid[IndexPosition(i, y)];

                if (CTile == GameTile.gt_blank) complete = true;
                else if (CTile != Tile) discovered++;
                else if (CTile == Tile && discovered > 0)
                {
                    complete = true;
                    Result.Add(0);
                    Result.Add(i);
                    Result.Add(y);
                }
                else complete = true;
            }

            //  Left

            complete = false;
            discovered = 0;
            for (int i = x - 1; i >= 0 && !complete; i--)
            {
                GameTile CTile = Grid[IndexPosition(i, y)];

                if (CTile == GameTile.gt_blank) complete = true;
                else if (CTile != Tile) discovered++;
                else if (CTile == Tile && discovered > 0)
                {
                    complete = true;
                    Result.Add(1);
                    Result.Add(i);
                    Result.Add(y);
                }
                else complete = true;
            }

            //  Top

            complete = false;
            discovered = 0;
            for (int i = y - 1; i >= 0 && !complete; i--)
            {
                GameTile CTile = Grid[IndexPosition(x, i)];

                if (CTile == GameTile.gt_blank) complete = true;
                else if (CTile != Tile) discovered++;
                else if (CTile == Tile && discovered > 0)
                {
                    complete = true;
                    Result.Add(2);
                    Result.Add(x);
                    Result.Add(i);
                }
                else complete = true;
            }

            //  Bottom

            complete = false;
            discovered = 0;
            for (int i = y + 1; i < OrthelloSize && !complete; i++)
            {
                GameTile CTile = Grid[IndexPosition(x, i)];

                if (CTile == GameTile.gt_blank) complete = true;
                else if (CTile != Tile) discovered++;
                else if (CTile == Tile && discovered > 0)
                {
                    complete = true;
                    Result.Add(3);
                    Result.Add(x);
                    Result.Add(i);
                }
                else complete = true;
            }

            //  Bottom Right

            complete = false;
            discovered = 0;
            for (int i = x+1, j = y+1; i<OrthelloSize && j<OrthelloSize && !complete; i++,j++)
            {
                GameTile CTile = Grid[IndexPosition(i, j)];

                if (CTile == GameTile.gt_blank) complete = true;
                else if (CTile != Tile) discovered++;
                else if (CTile == Tile && discovered > 0)
                {
                    complete = true;
                    Result.Add(4);
                    Result.Add(i);
                    Result.Add(j);
                }
                else complete = true;
            }

            //  Bottom Left

            complete = false;
            discovered = 0;
            for (int i = x - 1, j = y + 1; i >= 0 && j < OrthelloSize && !complete; i--,j++)
            {
                GameTile CTile = Grid[IndexPosition(i, j)];

                if (CTile == GameTile.gt_blank) complete = true;
                else if (CTile != Tile) discovered++;
                else if (CTile == Tile && discovered > 0)
                {
                    complete = true;
                    Result.Add(5);
                    Result.Add(i);
                    Result.Add(j);
                }
                else complete = true;
            }

            //  Top Left

            complete = false;
            discovered = 0;
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0 && !complete; i--,j--)
            {
                GameTile CTile = Grid[IndexPosition(i, j)];

                if (CTile == GameTile.gt_blank) complete = true;
                else if (CTile != Tile) discovered++;
                else if (CTile == Tile && discovered > 0)
                {
                    complete = true;
                    Result.Add(6);
                    Result.Add(i);
                    Result.Add(j);
                }
                else complete = true;
            }

            //  Top Right

            complete = false;
            discovered = 0;
            for (int i = x + 1, j = y - 1; i < OrthelloSize && j >= 0 && !complete; i++,j--)
            {
                GameTile CTile = Grid[IndexPosition(i, j)];

                if (CTile == GameTile.gt_blank) complete = true;
                else if (CTile != Tile) discovered++;
                else if (CTile == Tile && discovered > 0)
                {
                    complete = true;
                    Result.Add(7);
                    Result.Add(i);
                    Result.Add(j);
                }
                else complete = true;
            }

            return Result;
        }

        private void TurnTiles(int x, int y, List<int> Positions, GameTile ChangeTo )
        {
            for (int i = 0; i < Positions.Count; i += 3)
            {
                switch ( Positions[i] )
                {
                    case 0: // Right
                        {
                            for (int k = Positions[i + 1]; k > x; k--)
                                SetTile(k, Positions[i + 2], ChangeTo);
                        } break;
                    case 1: // Left
                        {
                            for (int k = Positions[i + 1]; k < x; k++)
                                SetTile(k, Positions[i + 2], ChangeTo);
                        } break;
                    case 2: // Top
                        {
                            for (int k = Positions[i + 2]; k < y; k++)
                                SetTile(Positions[i + 1], k, ChangeTo);
                        } break;
                    case 3: // Bottom
                        {
                            for (int k = Positions[i + 2]; k > y; k--)
                                SetTile(Positions[i + 1], k, ChangeTo);
                        } break;
                    case 4: // Bottom Right
                        {
                            for (int k = Positions[i + 1], l = Positions[ i + 2 ]; k > x && l > y; k--, l--)
                                SetTile(k, l, ChangeTo);
                        } break;
                    case 5: // Bottom Left
                        {
                            for (int k = Positions[i + 1], l = Positions[i + 2]; k < x && l > y; k++, l--)
                                SetTile(k, l, ChangeTo);
                        } break;
                    case 6: // Top Left
                        {
                            for (int k = Positions[i + 1], l = Positions[i + 2]; k < x && l < y; k++, l++)
                                SetTile(k, l, ChangeTo);
                        } break;
                    case 7: // Top Right
                        {
                            for (int k = Positions[i + 1], l = Positions[i + 2]; k > x && l < y; k--, l++)
                                SetTile(k, l, ChangeTo);
                        } break;
                }
            }
            SetTile(x, y, ChangeTo);
        }

        private int CountTurns(int x, int y, List<int> Positions )
        {
            int Result = 0;
            for (int i = 0; i < Positions.Count; i += 3)
            {
                switch (Positions[i])
                {
                    case 0:
                    case 1: Result += Math.Abs(Positions[i + 1] - x); break;
                    case 2:
                    case 3: 
                    case 4:
                    case 5:
                    case 6:
                    case 7: Result += Math.Abs(Positions[i + 2] - y); break;
                }
            }

            return Result;
        }

        private List<GridMove> GenerateMoves(GameTile Tile)
        {
            List<GridMove> Result = new List<GridMove>();

            for (int i = 0; i < OrthelloSize; i++)
            {
                for (int j = 0; j < OrthelloSize; j++)
                {
                    if (Grid[IndexPosition(j, i)] == GameTile.gt_blank)
                    {
                        List<int> Moves = FindPairsThatSurround(j, i, Tile);
                        int Qual = CountTurns(j, i, Moves);

                        if (Qual > 0) {
                            GridMove CMove = new GridMove();
                            CMove.PosX = j;
                            CMove.PoxY = i;
                            CMove.Moves = Moves;
                            CMove.Quality = Qual;
                        
                            Result.Add(CMove);
                        }
                    }
                }
            }

            Result.Sort();
            return Result;
        }

        private void tbxPosX_TextChanged(object sender, EventArgs e)
        {

        }

    }
}

