import React, { useEffect, useState } from "react";
import "./App.css";
import Playlist from "./Playlist";

const App = () => {
  const [playlists, setPlaylists] = useState([]);
  const [search, setSearch] = useState("");

  const [genre, setGenre] = useState("");
  const [duration, setDuration] = useState("");

  const API_URL = `http://localhost:5000/api/Playlists/search?query=${search}&genre=${genre}&duration=${duration}`;

  useEffect(() => {
    getPlaylists();
  }, [search]); //every time the search runs (is changed) useEffect runs.

  const getPlaylists = async () => {
    const response = await fetch(API_URL);
    const data = await response.json();

    setPlaylists(data);
  };

  const updateSearch = (e) => {
    setSearch(e.target.value);
  };

  return (
    <div className="App">
      <form className="search-form">
      <label className="search-label">Search</label>
        <input
          className="search-bar"
          type="text"
          value={search}
          onChange={updateSearch}
        />
      </form>
      <div className="playlists">
      {playlists.map((x) => (
        <Playlist
          key={x.id}
          title={x.title}
          duration={x.durationInHours}
          rank={x.rank}
          pixabayImage={x.pixabayImage}
        />
      ))}
      </div>
    </div>
  );
};

export default App;