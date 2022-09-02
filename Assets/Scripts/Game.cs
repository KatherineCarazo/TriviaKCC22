using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Game : MonoBehaviour
{
    public Dificultad[] bancoPreguntas;

    public TextMeshProUGUI enunciado;

    public Text[] respuestas;

    public int nivelPregunta;

    public Pregunta preguntaActual;

    public PanelComplementario panelComplementario;

    //Panel Complementario
    public Button[] btnRespuesta;

    void Start()
    {
        nivelPregunta = 0;
        cargarBancoPreguntas();
        setPregunta();
    }

    public void setPregunta()
    {
        int preguntaRandom =
            Random.Range(0, bancoPreguntas[nivelPregunta].preguntas.Length);
        preguntaActual =
            bancoPreguntas[nivelPregunta].preguntas[preguntaRandom];
        enunciado.text = preguntaActual.enunciado;

        for (int i = 0; i < respuestas.Length; i++)
        {
            respuestas[i].text = preguntaActual.respuestas[i].texto;
        }
    }

    public void cargarBancoPreguntas()
    {
        try
        {
            bancoPreguntas =
                JsonConvert
                    .DeserializeObject<Dificultad[]>(File
                        .ReadAllText(Application.streamingAssetsPath +
                        "/QuestionBank.json"));
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
            enunciado.text = ex.Message;
        }
    }

    public void evaluarPregunta(int respuestaJugador)
    {
        if (respuestaJugador == preguntaActual.respuestaCorrecta)
        {
            nivelPregunta++;
            if (nivelPregunta == bancoPreguntas.Length)
            {
                SceneManager.LoadScene("Gane");
            }
            else
            {
                try
                {
                    panelComplementario.desplegar();
                }
                catch (System.Exception ex)
                {
                    Debug
                        .Log("Olvidaste configurar el panel cpmplementario: " +
                        ex.Message);
                }
            }
        }
        else{
            SceneManager.LoadScene("Perdida");
        }
    }
}
