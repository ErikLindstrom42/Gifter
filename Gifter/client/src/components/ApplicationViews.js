import React, { useContext, useState } from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import PostList from "./PostList";
import PostForm from "./PostForm";
import PostDetails from "./PostDetails";
import PostSearch from "./PostSearch";
import PostUser from "./PostUser";
import { UserProfileContext } from "../providers/UserProfileProvider";

const ApplicationViews = () => {
  const { isLoggedIn } = useContext(UserProfileContext);
  return (
    <Switch>

      <Route path="/" exact>
        {isLoggedIn ? <PostList /> : <Redirect to="/login" />}
      </Route>

      <Route path="/add">
        {isLoggedIn ? <PostDetails /> : <Redirect to="/login" />}
      </Route>
      <Route path="/" exact>
        <PostList />
      </Route>

      <Route path="/posts/add">
        <PostForm />
      </Route>

      <Route path="/posts/:id">{/* TODO: Post Details Component */}
        <PostDetails />
      </Route>

      <Route path="/users/:id(\d+)">
        <PostUser />
      </Route>

      <Route path="/posts/search/">
        <PostSearch />
      </Route>
    </Switch>
  );
};

export default ApplicationViews;