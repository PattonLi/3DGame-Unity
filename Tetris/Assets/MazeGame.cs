using UnityEngine;

public class MazeGame : MonoBehaviour
{
    /* model */

    // 迷宫的长和宽
    private int mazeWidth = 10;
    private int mazeHeight = 10;
    // 存储是路还是墙壁
    private bool[,] mazeData;
    // 存储人物的位置
    private Vector2 playerPosition;
    // 移动方向
    private enum moveDirection
    {
        up,
        down,
        left,
        right
    };
    // 出口位置
    private int[] target = { 0, 0 };
    // 墙壁
    public Texture2D image_wall;
    // 地面
    public Texture2D image_ground;
    // 人物
    public Texture2D image_player;
    // 出口
    public Texture2D image_target;

    /* system start */
    private void Start()
    {
        GenerateMaze();
        playerPosition = new Vector2(1, 1);
    }
    /* controller */

    // 生成迷宫
    private void GenerateMaze()
    {
        mazeData = new bool[mazeWidth, mazeHeight];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                mazeData[x, y] = true; // 将所有位置默认设置为墙壁
            }
        }

        DFS(1, 1); // 深度优先搜索构建迷宫

        // 设置出口
        for (int i = mazeHeight - 1; i >= 0; i--)
        {
            if (mazeData[mazeWidth - 2, i] == false)
            {
                mazeData[mazeWidth - 1, i] = false;
                target = new int[2] { mazeWidth - 1, i };
                break;
            }
        }
    }

    private void DFS(int x, int y)
    {
        mazeData[x, y] = false; // 将当前位置设置为路径

        // 随机排序4个方向
        int[] directions = new int[] { 0, 1, 2, 3 };
        for (int i = 0; i < directions.Length; i++)
        {
            int temp = directions[i];
            int randomIndex = Random.Range(i, directions.Length);
            directions[i] = directions[randomIndex];
            directions[randomIndex] = temp;
        }

        foreach (int direction in directions)
        {
            int newX = x;
            int newY = y;

            if (direction == 0) // 向上移动
            {
                newY -= 2;
            }
            else if (direction == 1) // 向下移动
            {
                newY += 2;
            }
            else if (direction == 2) // 向左移动
            {
                newX -= 2;
            }
            else if (direction == 3) // 向右移动
            {
                newX += 2;
            }

            // 检验探索位置是否合理
            if (newX >= 0 && newY >= 0 && newX <= mazeWidth - 1 && newY <= mazeHeight - 1 && mazeData[newX, newY] == true)
            {
                int wallX = x + (newX - x) / 2;
                int wallY = y + (newY - y) / 2;

                mazeData[wallX, wallY] = false; // 打通墙壁
                if (newX != 0 && newX != mazeWidth - 1 && newY != 0 && newY != mazeHeight - 1)
                {
                    DFS(newX, newY); // 递归地探索下一个位置
                }

            }
        }
    }

    // 移动位置
    private void Move(moveDirection md)
    {
        if (md == moveDirection.up)
        {
            playerPosition.y--;
        }
        else if (md == moveDirection.down)
        {
            playerPosition.y++;
        }
        else if (md == moveDirection.left)
        {
            playerPosition.x--;
        }
        else
        {
            playerPosition.x++;
        }

        // Debug.Log(playerPosition);
        // Debug.Log(new Vector2(target[0], target[1]));
        // Debug.Log(playerPosition.Equals(new Vector2(target[0], target[1])));
    }

    // 设置迷宫大小
    private void SetMazeSize(int width, int height)
    {
        mazeHeight = height;
        mazeWidth = width;
        GenerateMaze();
        playerPosition = new Vector2(1, 1);
    }
    // 默认设置
    private void Reset()
    {
        SetMazeSize(6, 6);
    }
    private bool IsWin()
    {
        if (playerPosition.Equals(new Vector2(target[0], target[1])))
        {
            return true;
        }
        return false;
    }

    /* view */
    private void OnGUI()
    {
        // 创建一个自定义样式
        GUIStyle style_wall = new GUIStyle(GUI.skin.label);
        style_wall.normal.background = image_wall;
        GUIStyle style_ground = new GUIStyle(GUI.skin.label);
        style_ground.normal.background = image_ground;
        GUIStyle style_player = new GUIStyle(GUI.skin.label);
        style_player.normal.background = image_player;
        GUIStyle style_target = new GUIStyle(GUI.skin.label);
        style_target.normal.background = image_target;

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                // 画迷宫视图
                Rect rect = new Rect(x * 20 + 20, y * 20 + 20, 20, 20);
                GUI.Box(rect, GUIContent.none, mazeData[x, y] == true ? style_wall : style_ground);
                // 画玩家视图
                if (playerPosition.x == x && playerPosition.y == y)
                {
                    GUI.Label(rect, GUIContent.none, style_player);
                }
                // 画出口
                GUI.Label(new Rect(target[0] * 20 + 20, target[1] * 20 + 20, 20, 20), GUIContent.none, style_target);
            }
        }

        // 玩家控制按钮
        if (GUI.Button(new Rect(20, mazeHeight * 20 + 30, 100, 30), "上"))
        {
            if (playerPosition.y > 0 && !mazeData[(int)playerPosition.x, (int)playerPosition.y - 1])
            {
                Move(moveDirection.up);
            }
        }

        if (GUI.Button(new Rect(130, mazeHeight * 20 + 30, 100, 30), "下"))
        {
            if (playerPosition.y < mazeHeight - 1 && !mazeData[(int)playerPosition.x, (int)playerPosition.y + 1])
            {
                Move(moveDirection.down);
            }
        }

        if (GUI.Button(new Rect(240, mazeHeight * 20 + 30, 100, 30), "左"))
        {
            if (playerPosition.x > 0 && !mazeData[(int)playerPosition.x - 1, (int)playerPosition.y])
            {
                Move(moveDirection.left);
            }
        }

        if (GUI.Button(new Rect(350, mazeHeight * 20 + 30, 100, 30), "右"))
        {
            if (playerPosition.x < mazeWidth - 1 && !mazeData[(int)playerPosition.x + 1, (int)playerPosition.y])
            {
                Move(moveDirection.right);
            }
        }

        // 判断胜利条件
        if (IsWin())
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 30;
            style.normal.textColor = Color.red;
            Rect rect = new Rect(350, mazeHeight * 20 + 80, 120, 50);
            GUI.Label(rect, "you win!", style);
        }
        // 设置迷宫大小条
        int w = (int)GUI.HorizontalSlider(new Rect(20, mazeHeight * 20 + 80, 100, 30), mazeWidth, 5.0f, 30.0f);
        int h = (int)GUI.HorizontalSlider(new Rect(130, mazeHeight * 20 + 80, 100, 30), mazeHeight, 5.0f, 30.0f);
        if (w != mazeWidth || h != mazeHeight)
        {
            SetMazeSize(w, h);
        }
        // 复原按钮
        if (GUI.Button(new Rect(240, mazeHeight * 20 + 80, 60, 30), "默认设置"))
        {
            Reset();
        }
    }
}