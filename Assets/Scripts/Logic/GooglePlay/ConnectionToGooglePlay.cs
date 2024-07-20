using DZGames.Football.Services;
using UnityEngine;
using VContainer;

namespace DZGames.Football.GooglePlay
{
    public class ConnectionToGooglePlay : MonoBehaviour
    {
        private IGooglePlayService _googlePlayService;

        [Inject]
        private void Construct(IGooglePlayService googlePlayService) =>
            _googlePlayService = googlePlayService;

        private void Start()
        {
            if (_googlePlayService.Authenticated == false)
                _googlePlayService.SignGooglePlay();
        }
    }
}