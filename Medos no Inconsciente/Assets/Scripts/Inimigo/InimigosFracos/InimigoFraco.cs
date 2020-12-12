﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class InimigoFraco : MonoBehaviour
{
    private ModoAbstrato estadoAtual;
    public Transform player;
    public NavMeshAgent naveMesh;
    public float disMinSeguir, distanciaPlayer, distanciaAtaque, cronometroAtaque, tempoAtacar, espereLaser = 1f;
    public bool estaAtacando = false, areaAtaque = false;
    public GameObject laser;
    public Animator animInimigo;

    public ModoAbstrato EstadoAtual
    {
        get {return estadoAtual;}
    }

    public readonly ModoAtacar EstadoAtacar = new ModoAtacar();
    public readonly ModoPatrulhar EstadoPatrulha = new ModoPatrulhar();

    void Start()
    {
        TransicaoParaEstado(EstadoAtacar);
    }

    void Update()
    {
        Vector3 direcao = player.transform.position - transform.position;
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        transform.rotation = Quaternion.Slerp(transform.rotation, novaRotacao, Time.deltaTime * 1);

        naveMesh.updateRotation = false;

        estadoAtual.Update(this);
    }

    public void TransicaoParaEstado(ModoAbstrato estado)
    {
        estadoAtual = estado;
        estadoAtual.EstadoEntrada(this);
    }
}
