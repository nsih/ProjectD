using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFuncManager : MonoBehaviour
{




    public void SpecialFuncs(int funcID)
    {
        if(funcID == 0 )
        {
            Debug.Log(funcID);
        }

        else if(funcID == 1)    //해당 스테이지 맵 밝히기
        {
            Debug.Log(funcID);
        }

        else if(funcID == 2)    //맵 클리어시 골드 +5
        {
            Debug.Log(funcID);
        }

        else if(funcID == 3)    //Dash마다 뒤에 폭발
        {   
            Debug.Log(funcID);
        }

        else if(funcID == 4)    //이번에 다 이동가능
        {
            Debug.Log(funcID);
        }

        else if(funcID == 5)    //천보
        {
            Debug.Log(funcID);
        }

        else
        {
            Debug.Log(funcID);
        }
    }
}
