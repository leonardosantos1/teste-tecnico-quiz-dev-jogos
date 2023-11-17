using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;


public class GameController : MonoBehaviour
{

    [SerializeField] private Question[] questions;

    [SerializeField] private int correctAnswers = 0;
    private int currentQuestionNumber;

    [SerializeField] private Button alternativeA;
    [SerializeField] private Button alternativeB;
    [SerializeField] private Button alternativeC;
    [SerializeField] private Button alternativeD;

    [SerializeField] private Button alternativeParentA;
    [SerializeField] private Button alternativeParentB;
    [SerializeField] private Button alternativeParentC;
    [SerializeField] private Button alternativeParentD;

    [SerializeField] private TMP_Text correctAnswersText;
    [SerializeField] private TMP_Text currentQuestionNumberText;
    [SerializeField] private TMP_Text askQuestionText;

    [SerializeField] private TMP_Text textButtonA;
    [SerializeField] private TMP_Text textButtonB;
    [SerializeField] private TMP_Text textButtonC;
    [SerializeField] private TMP_Text textButtonD;

    [SerializeField] private TMP_Text feedBackFinishText;
    [SerializeField] private GameObject totalQuestionsFinish;

    [SerializeField] private GameObject totalQuestions;

    [SerializeField] private int currentQuestionScriptableObject = 0;

    [SerializeField] private string[] feedbackMessage;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correctAnswerAudioClip;

    // Start is called before the first frame update
    void Start()
    {

        currentQuestionNumber = 1;

        alternativeA.onClick.AddListener(() => ClickOnAnswer(alternativeA));
        alternativeB.onClick.AddListener(() => ClickOnAnswer(alternativeB));
        alternativeC.onClick.AddListener(() => ClickOnAnswer(alternativeC));
        alternativeD.onClick.AddListener(() => ClickOnAnswer(alternativeD));


        textButtonA = alternativeA.GetComponentInChildren<TMP_Text>();
        textButtonB = alternativeB.GetComponentInChildren<TMP_Text>();
        textButtonC = alternativeC.GetComponentInChildren<TMP_Text>();
        textButtonD = alternativeD.GetComponentInChildren<TMP_Text>();

        SetAskAndAlternatives();

    }

    void Update()
    {

        if (currentQuestionNumberText.text.Equals("4"))
        {

            correctAnswersText.text = correctAnswers.ToString();

            alternativeParentA.gameObject.SetActive(false);
            alternativeParentB.gameObject.SetActive(false);
            alternativeParentC.gameObject.SetActive(false);
            alternativeParentD.gameObject.SetActive(false);

            currentQuestionNumberText.gameObject.SetActive(false);
            askQuestionText.gameObject.SetActive(false);
            totalQuestions.SetActive(false);

            correctAnswersText.gameObject.SetActive(true);
            totalQuestionsFinish.SetActive(true);
            feedBackFinishText.gameObject.SetActive(true);


            switch (correctAnswers)
            {
                case 3:
                    feedBackFinishText.text = feedbackMessage[2];
                    break;
                case 2:
                    feedBackFinishText.text = feedbackMessage[1];
                    break;
                default:
                    feedBackFinishText.text = feedbackMessage[0];
                    break;
            }

            StartCoroutine("GameIsFinish");

        }
    }

    public void ClickOnAnswer(Button button)
    {
        TMP_Text textButtonAnswer = button.GetComponentInChildren<TMP_Text>();

        if (textButtonAnswer.text.Equals(questions[currentQuestionScriptableObject].answer))
        {
            correctAnswers++;
            audioSource.PlayOneShot(correctAnswerAudioClip);

        }

        currentQuestionNumber++;
        currentQuestionNumberText.text = currentQuestionNumber.ToString();
        currentQuestionScriptableObject++;
        SetAskAndAlternatives();

    }

    IEnumerator GameIsFinish()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);

    }

    public void SetAskAndAlternatives()
    {

        textButtonA.text = questions[currentQuestionScriptableObject].alternatives[0];
        textButtonB.text = questions[currentQuestionScriptableObject].alternatives[1];
        textButtonC.text = questions[currentQuestionScriptableObject].alternatives[2];
        textButtonD.text = questions[currentQuestionScriptableObject].alternatives[3];

        askQuestionText.text = questions[currentQuestionScriptableObject].ask;
    }

}

