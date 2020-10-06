import React, { useState, useEffect, createContext } from "react";
import { Spinner } from "reactstrap";
import * as firebase from "firebase/app";
import "firebase/auth";

export const UserProfileContext = createContext();

export const UserProfileProvider = (props) => {
  const [userProfiles, setUserProfiles] = useState([]);
  const apiUrl = "/api/userprofile";

const userProfile = sessionStorage.getItem("userProfile");
const [isLoggedIn, setIsLoggedIn] = useState(userProfile != null);

const [isFirebaseReady, setIsFirebaseReady] = useState(false);
useEffect(() =>{
    firebase.auth().onAuthStateChanged((u) => {
        setIsFirebaseReady(true);
    });
}, []);

const login = (email, pw) => {
    return firebase.auth().signInWithEmailAndPassword(email, pw)
    .then((signInResponse) => getUserProfile(signInResponse.user.uid))
    .then((userProfile) => {
        sessionStorage.setItem("userProfile", JSON.stringify(userProfile));
        setIsLoggedIn(true);
    });
};

const logout = () => {
    return firebase.auth().signOut()
    .then(() => {
        sessionStorage.clear();
        setIsLoggedIn(false);
    });
};

const register = (userProfile, password) => {
    return firebase.auth().createUserWithEmailAndPassword(userProfile.email, password)
    .then((createResponse) => saveUser({ ...userProfile, firebaseUserId: createResponse.user.uid}))
    .then((savedUserProfile) => {
        sessionStorage.setItem("userProfile", JSON.stringify(savedUserProfile));
        setIsLoggedIn(true);
    });
};

const getToken = () => firebase.auth().currentUser.getIdToken();

const getUserProfile = (firebaseUserId) => {
    return getToken().then((token) =>
    fetch(`${apiUrl}/${firebaseUserId}`, {
        method: "GET",
        headers: {
            Authorization: `Bearer ${token}`
        }
    }).then(resp => resp.json()));
};

const saveUser = (userProfile) => {
    return getToken().then((token) =>
    fetch(apiUrl, {
        method: "POST",
        headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json"
        },
        body: JSON.stringify(userProfile)
    }).then(resp => resp.json()));
};


//Stolen from post and edited
//   const getAllUserProfiles = () => {
//     console.log("getting all the profiles")
//     return fetch("/api/userProfile")
//       .then((res) => res.json())
//       .then(setUserProfiles);
//   };
//Stolen from post and edited
//   const getUserProfilesByUserId = (id) => {
//     return fetch(`/api/post/GetUserProfilesByUserId/${id}`)
//     .then((res) => res.json())
//     .then(setPosts);
//   }
//Stolen from post
//   const getPost = (id) => {
//     console.log("getting the post")
//     return fetch(`/api/post/${id}`).then((res) => res.json());
// };
//Stolen from post
// const searchPosts = (searchValue, userProfileId) => {
//   return fetch(`/api/post/search?q=${searchValue}&user=${userProfileId}`)
//   .then((res) => res.json())
//   .then(setPosts);
// }
//Stolen from post
//   const addPost = (post) => {
//     return fetch("/api/post", {
//       method: "POST",
//       headers: {
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(post),
//     });
//   };

  return (
    <UserProfileContext.Provider value={{ isLoggedIn, login, logout, register, getToken}}>
      {isFirebaseReady
      ? props.children
      : <Spinner className="app-spinner dark"/>}
    </UserProfileContext.Provider>
  );
};