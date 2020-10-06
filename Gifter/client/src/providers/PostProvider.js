import React, { useState } from "react";

export const PostContext = React.createContext();

export const PostProvider = (props) => {
  const [posts, setPosts] = useState([]);

  const getAllPosts = () => {
    console.log("getting all the posts")
    return fetch("/api/post")
      .then((res) => res.json())
      .then(setPosts);
  };

  const getPostsByUserId = (id) => {
    return fetch(`/api/post/GetPostsByUserId/${id}`)
    .then((res) => res.json())
    .then(setPosts);
  }

  const getPost = (id) => {
    console.log("getting the post")
    return fetch(`/api/post/${id}`).then((res) => res.json());
};

const searchPosts = (searchValue, userProfileId) => {
  return fetch(`/api/post/search?q=${searchValue}&user=${userProfileId}`)
  .then((res) => res.json())
  .then(setPosts);
}

  const addPost = (post) => {
    return fetch("/api/post", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(post),
    });
  };

  return (
    <PostContext.Provider value={{ posts, getAllPosts, addPost, getPost, searchPosts, getPostsByUserId }}>
      {props.children}
    </PostContext.Provider>
  );
};