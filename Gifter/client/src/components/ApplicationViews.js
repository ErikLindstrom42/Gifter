import React from "react";
import { Switch, Route } from "react-router-dom";
import PostList from "./PostList";
import PostForm from "./PostForm";
import PostDetails from "./PostDetails";
import PostSearch from "./PostSearch";
import PostUser from "./PostUser";

const ApplicationViews = () => {
  return (
    <Switch>
      <Route path="/" exact>
        <PostList />
      </Route>

      <Route path="/posts/add">
        <PostForm />
      </Route>

      <Route path="/posts/:id">{/* TODO: Post Details Component */}
      <PostDetails />
      </Route>

      <Route path ="/users/:id(\d+)"> 
      <PostUser />
      </Route>

      <Route path="/posts/search/">
      <PostSearch />
      </Route>
    </Switch>
  );
};

export default ApplicationViews;