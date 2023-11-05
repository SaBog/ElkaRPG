using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int health;
    int mana;
    int damage;
    public int x, y;

    //���������?
    [SerializeField]
    Map map; //����� ��������?


    public void Movement() //�����������
    {
        if (Input.GetKeyDown(KeyCode.W) && IsNotCollided(x,y+1)){
            y++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.S) && IsNotCollided(x, y-1))
        {
            y--;
            return;
        }
        if (Input.GetKeyDown(KeyCode.D) && IsNotCollided(x+1, y))
        {
            x++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.A) && IsNotCollided(x-1, y))
        {
            x--;
            return;
        }

    }

    private bool IsNotCollided(int x, int y) //������������ �� ������
    {
        if(map.interior[x,y] == 1)
        {
            return false;
        }
        return true;
    }

    private void CheckTileTrigger()
    {
        //�������� �������� ��������� ������ : ������, ������
        return;
    }

    private void MoveOnNextTile()
    {
        this.gameObject.transform.position = new Vector3(x+0.5f, y+0.5f);

    }

    //��������� ������ �� ����������

    // Start is called before the first frame update
    void Start()
    {
        MoveOnNextTile();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckTileTrigger();
        MoveOnNextTile();
    }
}
