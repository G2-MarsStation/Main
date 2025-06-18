using UnityEngine;

public class AtivarParticulaNaCena : MonoBehaviour
{
    public GameObject particula1;
    public GameObject particula2;
    public GameObject particula3;
    public GameObject particula4;
    public GameObject particula5;
    public GameObject particula6;
    public GameObject particula7;
    public GameObject particula8;
    public GameObject particula9;

    public float tempoParaAtivar = 2f;
    public float tempoEntreCiclos = 5f;
    public float tempoAtivo = 3f;

    private Vector3 ultimaPosicao;
    private float tempoParado = 0f;
    private bool ativado = false;
    private bool cicloRodando = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 posAtual = other.transform.position;

            if (Vector3.Distance(posAtual, ultimaPosicao) < 0.01f)
            {
                tempoParado += Time.deltaTime;
            }
            else
            {
                tempoParado = 0f;
            }

            ultimaPosicao = posAtual;

            if (!ativado && tempoParado >= tempoParaAtivar)
            {
                ativado = true;
                cicloRodando = true;
                StartCoroutine(CicloParticulas());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tempoParado = 0f;
            ativado = false;
            cicloRodando = false;
            ultimaPosicao = Vector3.zero;
        }
    }

    private System.Collections.IEnumerator CicloParticulas()
    {
        while (cicloRodando)
        {
            AtivarTodas(true);
            yield return new WaitForSeconds(tempoAtivo);
            AtivarTodas(false);

            AtivarTodas(true);
            yield return new WaitForSeconds(tempoAtivo);
            AtivarTodas(false);

            yield return new WaitForSeconds(tempoEntreCiclos);
        }
    }

    private void AtivarTodas(bool estado)
    {
        if (particula1 != null) particula1.SetActive(estado);
        if (particula2 != null) particula2.SetActive(estado);
        if (particula3 != null) particula3.SetActive(estado);
        if (particula4 != null) particula4.SetActive(estado);
        if (particula5 != null) particula5.SetActive(estado);
        if (particula6 != null) particula6.SetActive(estado);
        if (particula7 != null) particula7.SetActive(estado);
        if (particula8 != null) particula8.SetActive(estado);
        if (particula9 != null) particula9.SetActive(estado);
    }
}
