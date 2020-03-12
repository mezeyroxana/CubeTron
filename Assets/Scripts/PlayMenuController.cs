using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayMenuController : MonoBehaviour
{
    public List<Button> player1Colors;
    public List<Button> player2Colors;
    public List<Material> colorMaterials;
    public Player1Data player1Data;
    public Player2Data player2Data;
    public TMP_InputField player1TextName;
    public TMP_InputField player2TextName;
    public Slider speedSlider;
    public Image planetImage;
    public Sprite cubeImage;
    public Sprite cuboidImage;

    private int speedValue;
    private Button player1ChoosedColor;
    private Button player2ChoosedColor;
    private int planetNumber;

    void Start()
    {
        player1Data.playerName = "PLAYER1";
        player2Data.playerName = "PLAYER2";
        planetNumber = 1;
    }

    public void Player1ColorChooser(Button pressedButton)
    {
        int toBeDisabled = -1;
        for (int i = 0; i < player1Colors.Count; i++)
        {
            if (!pressedButton.Equals(player1Colors[i]))
            {
                ColorBlock colors = player1Colors[i].colors;
                colors.normalColor = new Color32(255, 255, 255, 100);
                player1Colors[i].colors = colors;
                player2Colors[i].interactable = true;
            }
            else toBeDisabled = i;
        }
        ColorBlock pressedColors = pressedButton.colors;
        pressedColors.normalColor = new Color32(255, 255, 255, 255);
        pressedButton.colors = pressedColors;
        player2Colors[toBeDisabled].interactable = false;
        player1ChoosedColor = pressedButton;
    }

    public void Player2ColorChooser(Button pressedButton)
    {
        int toBeDisabled = -1;
        for (int i = 0; i < player2Colors.Count; i++)
        {
            if (!pressedButton.Equals(player2Colors[i]))
            {
                ColorBlock colors = player2Colors[i].colors;
                colors.normalColor = new Color32(255, 255, 255, 100);
                player2Colors[i].colors = colors;
                player1Colors[i].interactable = true;
            }
            else toBeDisabled = i;
        }
        ColorBlock pressedColors = pressedButton.colors;
        pressedColors.normalColor = new Color32(255, 255, 255, 255);
        pressedButton.colors = pressedColors;
        player1Colors[toBeDisabled].interactable = false;
        player2ChoosedColor = pressedButton;
    }

    public void PlayButtonPressed()
    {
        speedValue = (int)speedSlider.value;
        float speedHelper = (float)1 / 4;
        GameData.speed = 1 + (speedHelper * speedValue);

        if (player1ChoosedColor != null)
            player1Data.color = colorMaterials.Find(x => x.name == player1ChoosedColor.name);
        else player1Data.color = colorMaterials.Find(x => x.name == "Pink");
        if (player2ChoosedColor != null)
            player2Data.color = colorMaterials.Find(x => x.name == player2ChoosedColor.name);
        else player2Data.color = colorMaterials.Find(x => x.name == "Cyan");

        if (!(player1TextName.text.Equals(string.Empty)))
            player1Data.playerName = player1TextName.text;
        if (!(player2TextName.text.Equals(string.Empty)))
            player2Data.playerName = player2TextName.text;

        player1Data.score = 0;
        player2Data.score = 0;
        GameData.round = 1;
        GameData.gameEnabled = true;
        SceneManager.LoadScene(planetNumber);
    }

    public void OnPlanetButtonClick(Button planetButton)
    {
        if (planetButton.name == "CubeButton")
        {
            planetImage.sprite = cubeImage;
            planetNumber = 1;
        }
        if (planetButton.name == "CuboidButton")
        {
            planetImage.sprite = cuboidImage;
            planetNumber = 2;
        }
    }
}
