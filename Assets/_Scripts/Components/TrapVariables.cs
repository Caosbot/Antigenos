using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrapVariables : MonoBehaviourPunCallbacks
{
    private Trap_Component trap;
    private void Start()
    {
        trap = gameObject.GetComponent<Trap_Component>();
    }

    [PunRPC]
    public void WorkingTrap()
    {
        trap.StartDelay();
    }
    public void TrapWork()
    {
        GetComponent<PhotonView>().RPC(nameof(WorkingTrap), RpcTarget.All);
    }
}
