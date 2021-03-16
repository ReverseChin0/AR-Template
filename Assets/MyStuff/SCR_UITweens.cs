using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * Francisco Emmanuel Castañeda López
 * Libreria de Tween para UI Extensible
 */

public enum TipoDeTween
{
    Mover,
    Escalar,
    Desvanecer,
    Saltar,
    Columpiar
}
public class SCR_UITweens : MonoBehaviour
{
    [SerializeField] private bool reproducirOnStart = false;
    [SerializeField] private bool reproducirOnEnable = false;
    [SerializeField] private bool usarPosicionInicial = false;
    [SerializeField] private bool unscaleTime = false;
    [SerializeField] private bool loops = false;
    [SerializeField] private float delay = 0.0f, duracion = 0.5f;
    [SerializeField] private TipoDeTween tipoTween = TipoDeTween.Mover;
    [SerializeField] private Ease tipoDeSuavizado = default;
    [SerializeField] Vector2 posInicial = default;
    [SerializeField] private Vector2 posFinal = default;
    RectTransform rectTransform = default;
    CanvasGroup cGroup = default;
    bool desactivando = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (tipoTween == TipoDeTween.Desvanecer)
        {
            cGroup = GetComponent<CanvasGroup>();
            if (cGroup == null)
                cGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        if (!usarPosicionInicial)
            posInicial = rectTransform.anchoredPosition;

        if (reproducirOnStart)
            Reproducir();
    }

    private void OnEnable()
    {
        if (reproducirOnEnable)
            Reproducir();
    }

    public void Reproducir()
    {
        switch (tipoTween)
        {
            case TipoDeTween.Mover:
                if (usarPosicionInicial)
                    rectTransform.anchoredPosition = posInicial;

                rectTransform.
                    DOAnchorPos(posFinal, duracion).
                    SetDelay(delay).
                    SetEase(tipoDeSuavizado).SetLoops(loops ? -1 : 0).
                    SetUpdate(unscaleTime);
                break;

            case TipoDeTween.Escalar:
                if (usarPosicionInicial)
                    rectTransform.localScale = new Vector3(posInicial.x, posInicial.y, 0);

                rectTransform.
                    DOScale(new Vector3(posFinal.x, posFinal.y, 0), duracion).
                    SetDelay(delay).
                    SetEase(tipoDeSuavizado).
                    SetLoops(loops ? -1 : 0).
                    SetUpdate(unscaleTime);
                break;

            case TipoDeTween.Desvanecer:
                if (usarPosicionInicial)
                    cGroup.alpha = posInicial.x;

                cGroup.
                    DOFade(posFinal.x, duracion).
                    SetDelay(delay).
                    SetEase(tipoDeSuavizado).
                    SetLoops(loops ? -1 : 0, LoopType.Yoyo).
                    SetUpdate(unscaleTime);
                break;

            case TipoDeTween.Saltar:
                if (usarPosicionInicial)
                    rectTransform.anchoredPosition = posInicial;

                rectTransform.
                    DOJumpAnchorPos(posFinal, 100.0f, 0, duracion).
                    SetDelay(delay).
                    SetLoops(loops ? -1 : 0);
                break;

            case TipoDeTween.Columpiar:
                if (usarPosicionInicial)
                    rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, posInicial.x));

                rectTransform.
                    DOLocalRotate(new Vector3(0, 0, posFinal.x), duracion).
                    SetEase(tipoDeSuavizado).
                    SetLoops(loops ? -1 : 0, LoopType.Yoyo).
                    SetUpdate(unscaleTime);
                break;

            default:
                break;
        }
    }

    public void Switch()
    {
        Vector2 temp = posInicial;
        posInicial = posFinal;
        posFinal = temp;
    }

    void InvertirYReproducir()
    {
        Switch();
        Reproducir();
    }

    public void Desactivar() 
    {
        if(gameObject.activeSelf)
        StartCoroutine(desactivar());
    }

    IEnumerator desactivar()
    {
        desactivando = true;
        InvertirYReproducir();
        yield return new WaitForSeconds(duracion);
        Switch();
        gameObject.SetActive(false);
        desactivando = false;
    }

    public void Activar()
    {
        if (!desactivando)
        {
            gameObject.SetActive(true);
        }
        else
        {
            StopAllCoroutines();
            desactivando = false;
            Switch();
            Reproducir();
        }
    }
}
