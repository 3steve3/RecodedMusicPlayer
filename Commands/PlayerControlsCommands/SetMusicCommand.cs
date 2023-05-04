using RecodedMusicPlayer.Models;
using RecodedMusicPlayer.Stores;
using RecodedMusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecodedMusicPlayer.Commands.PlayerControlsCommands
{
    public class SetMusicCommand : CommandBase
    {

        private readonly MusicPlayer _musicplayer;
        private readonly PlayerControlsViewModel playerVm;

        public SetMusicCommand(PlayerControlsViewModel playerControlsViewModel, MusicPlayer musicplayer)
        {
            _musicplayer=musicplayer;
            playerVm=playerControlsViewModel;
        }
        public override void Execute(object parameter)
        {
            MusicFile currentSong = ((MusicFile)parameter);

            try
            {
                _musicplayer.playerPlaying = true;

                playerVm.SongTitleText = "Song Title";
                playerVm.ArtistTitleText = "Song Artist";
                playerVm.AlbumCoverImage = null;
                playerVm.DurationSliderMaxValue = 0;
                playerVm.SongDuration = "0:00";

                

                if (App._navigationStore.CurrentViewModel is MainPageViewModel)
                {
                    ((MainPageViewModel)App._navigationStore.CurrentViewModel).MainPageUpdateViewCommand.Execute(currentSong);
                };
            }
            catch (Exception)
            {
                _musicplayer.playerCurrentSong = _musicplayer.GetMusicList().FirstOrDefault();
                playerVm.SetSongCommand.Execute(_musicplayer.GetMusicList().FirstOrDefault());
            }
        }

    }
}
