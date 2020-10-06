import React, { useContext, useEffect } from "react";
import { PostContext } from "../providers/PostProvider";
import Post from "./Post";
import { useParams } from "react-router-dom";

const PostUser = () => {
  const { posts, getPostsByUserId } = useContext(PostContext);
  const { id } = useParams();

  useEffect(() => {
    getPostsByUserId(id);
  }, []);


  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="cards-column">
          {posts.map((post) => (
            <Post key={post.id} post={post} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default PostUser;