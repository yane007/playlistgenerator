import React from "react";
import style from './playlist.module.css';

const Playlist = ({title, duration, rank, pixabayImage}) => {
    return(
        <div className={style.playlist}>
            <h1>{title}</h1>
            <p>{duration}</p>
            <p>{rank}</p>
            <img className="playlistImage" src='https://cdn.pixabay.com/photo/2020/11/04/07/52/pumpkin-5711688_960_720.jpg' alt=""/>
        </div>
    );
}

export default Playlist;