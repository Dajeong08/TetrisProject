using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tetromino : MonoBehaviour
{
    public static int Height = 20; // 높이 
    public static int Width = 10; // 너비 
    private static Transform[,] grid = new Transform[Width, Height]; 

    public float FallTime; // 현재 낙하 시간 (정적 변수로 설정)
    private float previousTime; // 블록 낙하

    public Vector3 RotationPoint; // 회전 포인트
    private bool isGameOver = false; // 게임 오버 변수
    public float requiredTime = 3f; // 3초

    void Update() 
    {
        if (isGameOver) // 게임 오버 상태일 때는 움직임을 멈추고 Restart 버튼을 활성화
        {
            return; // 게임 오버 상태에서는 Update()의 나머지 로직을 실행하지 않음
        }
        // 블록 이동
        HandleMovement(); // 움직임 함수 호출
        
        float FallTime = FallTimeManager.FallTime;
        // 블록 낙하
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? FallTime / 10 : FallTime)) // 꾹 누르면 빠르게 내려가기 
        {
            Move(0, -1); // 아래로 이동
            if (!ValidMove()) // 오브젝트의 범위를 확인하고 거짓이면
            {
                Move(0, 1); // 위로 이동
                AddToGrid();
                checkForLines(); // 줄 확인
                this.enabled = false;
                if (!isGameOver) // 게임오버가 아닐 때
                    FindObjectOfType<SpawnTetromino>().NewTetris(); // 오브젝트가 바닥에 닿을 때마다 새로운 오브젝트 생성
            }
            previousTime = Time.time;
            
        }
    }
    public void OnClickStop()
    {
        Debug.Log("Stop");
        isGameOver = true;
    }
    
    void HandleMovement() // 움직임 
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 왼쪽키를 눌렀을때 
        {
            Move(-1, 0); // 왼쪽으로 이동
            if (!ValidMove()) // 오브젝트의 범위를 확인하고 거짓이면
            {
                Move(1, 0); // 오른쪽으로 이동
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // 오른쪽키를 눌렀을 때 
        {
            Move(1, 0); // 오른쪽으로 이동
            if (!ValidMove()) // 오브젝트의 범위를 확인하고 거짓이면
            {
                Move(-1, 0); // 왼쪽으로 이동
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) // 위 키를 눌렀을 때 
        {
            Rotate(); // 회전 함수 호출 
        }
    }
    //움직임
    void Move(int xChange, int yChange) 
    {
        // 블록을 xChange, yChange 만큼 이동
        transform.position += new Vector3(xChange, yChange, 0);
    }

    //회전 
    void Rotate()
    {
        
        // 회전 기준점 확인
        Vector3 pivot = transform.TransformPoint(RotationPoint);

        // 블록을 회전 기준점을 중심으로 90도 회전
        transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), 90);
        // 범위 체크 후 오브젝트가 범위 이탈 시 실행 
        if (!ValidMove())
        {
            // 블록을 회전 기준점을 중심으로 -90도 회전 
            transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), -90);
        }
    }
    bool ValidMove() // 범위 확인 함수 
    {
        foreach (Transform children in transform) // 4씩 존재함.
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= Width || roundedY < 0 || roundedY >= Height)
            return false; // 그리드 좌표를 벗어나면 false

            if (grid[roundedX, roundedY] != null) // 그리드에 블록이 있으면 false
            return false; // 거짓반환 
        }
        return true; // 참 반환 
    }
    void AddToGrid() 
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedY < 20)
                grid[roundedX, roundedY] = children; // 해당 좌표를 그리드에 입력한다.
        }
    }
    void checkForLines() // 라인이 다 채워졌는지 확하는 함수 
    {
        int cnt = 0;
        for (int i = Height - 1; i >= 0; i--)
        {
            if (hasLine(i)) // 채워진 라인이 있는지 확인하고 있다면 삭제한 후 아래로 내려준다.
            {
                DeleteLine(i);
                RowDown(i);
                cnt++; // 
            }
        }
    }

    bool hasLine(int i) // 해당 라인이 모두 채워졌는지 확인하여 true, false 반환
    {
        for (int j = 0; j < Width; j++)
            if (grid[j, i] == null)

                return false;
        return true;
    }
    void DeleteLine(int i)
    {
        for (int j = 0; j < Width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < Height; y++)
            for (int j = 0; j < Width; j++)
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
    }
    
    
}



