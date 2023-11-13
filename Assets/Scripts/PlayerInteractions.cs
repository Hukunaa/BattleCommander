using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerInteractions() { }
    public static PlayerInteractions Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(PlayerInteractions)) as PlayerInteractions;

            return instance;
        }
        set
        {
            instance = value;
        }
    }


    private static PlayerInteractions instance;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    private bool _allowInteractions;
    public List<Interaction> InteractionsCards;
    private Interaction _selectedInteraction;

    private void Start()
    {
        GameManager.OnGameStateChanged += GameStateChanged;

        if(InteractionsCards.Count > 0)
            _selectedInteraction = InteractionsCards[0];
    }

    void GameStateChanged(GameManager.GameStateType _state)
    {
        if (_state == GameManager.GameStateType.Battle)
            _allowInteractions = true;
        else
            _allowInteractions = false;
    }

    public void SetInteraction(int _index)
    {
        if(_index < InteractionsCards.Count)
            _selectedInteraction = InteractionsCards[_index];
    }

    void Update()
    {
        if(_allowInteractions)
        {
            if(_selectedInteraction != null)
                _selectedInteraction.Interact();
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChanged;
    }
    public Interaction SelectedInteraction { get => _selectedInteraction; }
}
