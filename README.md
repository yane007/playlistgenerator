# RidePal Playlist Generator

![Project Image](./ReadmeImages/MyPlaylists01.png)

---

### Table of Contents
- [Description](#description)
- [How To Consume](#how-to-Consume)
- [How To Start Up](#how-to-start-up)
- [References](#references)
- [License](#license)
- [Authors Info](#authors-info)

---

## Description

 > RidePal Playlist Generator is a web application that enables our users to generate playlists for specific travel duration periods based on their preferred genres. 
 >
 >The application offers the option to browse playlists created by other users and
allows searching by title and filtering by total duration and genre tags.
 >
 >`Main Use case:` A user travelling from point A to point B wants to have something to listen to during the duration of the travel. The user wants to generate a track list based on his musical tastes.
 >
 >**`Public part:`** The public part is visible without authentication. This includes the application start page, the user login and user registration forms, as well as the list of all user generated playlists.
>
>**`Private part:`** People that are not authenticated cannot see any user specific details, neither they can interact with the website. They can only browse the playlists, see the tracks list and details of them and listen to the demo version of rach track.  
>Authenticated users have the ability to generate new playlists, control the generation algorithm and edit or delete their own existing playlists.  
>Editing existing playlists is limited to changing the title or associated genre
tags but does not include editing of the track list
>
>**`Admin part:`** System administrators can administer all major information objects in the system. On top of the regular user capabilities, the administrators have the following capabilities:
>* Able to edit/delete users and other administrators.
>* Able to edit/delete over the playlists.





#### Technologies

- [ASP.Net Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-3.1)
- [JavaScript](https://www.javascript.com/try)

[Back To The Top](#read-me-template)

---

# How To Consume
## Public part:
>### As a user the first steps are to Login / Register.
>![Project RegisterForm](./ReadmeImages/RegisterForm.png)
>
>### After successfully being authenticated you will be albe to ceate a playlist by specifying the start and end locations of your trip, the playlist's title and some percentage of a genre.
>![Project GeneratePlaylistForm](./ReadmeImages/GeneratePlaylistForm.png)
>
>### When the playlist finishes generating you will be redirected to `My Playlists`. Here you can browse all your generated playlists, you can open one by clicking on it.
>![Project MyPlaylistsAfterCreating](./ReadmeImages/MyPlaylistsAfterCreating.png)
>
>### Here you can play the entire playlist just by hitting the play button on the center.  
>### On the left is displayed the name on the playlist, by who it was made, its rank (the avare rank of all songs) and a paged list of all songs, their duration and preview link.
>### On the right you can control the volume, skip to next/previous track or just select a loaded track and play it.  
>### For editing a playlist click the `|Edit|` navigation link.
>![Project PlaylistEdit](./ReadmeImages/PlaylistEdit.png)
>
>### On the edit page you will be able to:
>- Make a playlist Public or Private.
>- Change its Title.
>- Change its icon link.
>- Delete it.  
>### After editing the Title or icon link the `Save` button must be clicked to save the changes, after saving it will redirect you to `My Playlists`. 
>![Project EditPlaylist](./ReadmeImages/EditPlaylist.png)
>
>### On the `Playlists` page are displayed all public playlists sorted by their rank. We can filter them by title, genre and/or duration.  
>- The search bar is used to search by title.
>- The dropdown next to the search bar allows us to select what genre we want to filter by.
>- The duration range filters by the playlists duration. The lowest you can select on the range slider is the shortest playlist, same goes for the highest you can select and longest playlist.
>
>![Project PlaylistsIndexMaxRange](./ReadmeImages/PlaylistsIndexMaxRange.png)
>![Project PlaylistsIndexRockFilter](./ReadmeImages/PlaylistsIndexRockFilter.png)
>
>### On the `Home` page the Top 3 playlists by ranking are displayed.
>![Project PlaylistsIndexRockFilter](./ReadmeImages/PlaylistsIndexRockFilter.png)


## Private part:
>### As an admin you can manage all user acces, even other admins. Navigate to `Users`
>![Project AdminUsersNavigation](./ReadmeImages/AdminUsersNavigation.png)
>
>### Admins also have the ability to delete other user's entire plalist, the delete button will only appear if an admin opens the playlist.
>![Project IndexTop3](./ReadmeImages/IndexTop3.png)


#### Installation



#### API Reference

```html
    <p>dummy code</p>
```
[Back To The Top](#read-me-template)

---

## References
[Back To The Top](#read-me-template)

---

## License

MIT License

Copyright (c) [2017] [James Q Quick]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

[Back To The Top](#read-me-template)

---

## Authors Info

- Twitter - [@jamesqquick](https://twitter.com/jamesqquick)
- Website - [James Q Quick](https://jamesqquick.com)

[Back To The Top](#read-me-template)