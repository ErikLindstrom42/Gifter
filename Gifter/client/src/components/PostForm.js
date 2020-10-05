import React, { useContext, useState } from "react";
import { Button, Form, FormGroup, Label, Input, Card, CardBody } from "reactstrap";
import { PostContext } from "../providers/PostProvider"
import {useHistory} from "react-router-dom"

const PostForm = () => {
    const { addPost } = useContext(PostContext);
    const [userProfileId, setUserProfileId] = useState("");
    const [imageUrl, setImageUrl] = useState("");
    const [title, setTitle] = useState("");
    const [caption, setCaption] = useState("");
    const [dateCreated, setDateCreated] = useState("");
    //const [post, setPost] = useState({ dateCreated: "", title:"", caption:"", userProfileId: "", imageUrl: "" })
    const [post, setPost] = useState({  })
    const history = useHistory();

    const submit = (evt) => {
        const post = {
            imageUrl,
            title,
            caption,
            dateCreated,
            userProfileId: +userProfileId
        };

        addPost(post).then((p) => {
            history.push("/");
        });
    };


    
    const handleFieldChange = (evt) => {
        const stateToChange = { ...post }
        stateToChange[evt.target.id] =
        evt.target.type === "number"
          ? parseInt(evt.target.value)
          : evt.target.value;
        setPost(stateToChange);
    };

    const constructNewPost = (evt) => {
        evt.preventDefault();
        addPost(post);
    }


    return (
        <div className="container pt-4">
          <div className="row justify-content-center">
            <Card className="col-sm-12 col-lg-6">
              <CardBody>
                <Form>
                  <FormGroup>
                    <Label for="userId">User Id (For Now...)</Label>
                    <Input
                      id="userId"
                      onChange={(e) => setUserProfileId(e.target.value)}
                    />
                  </FormGroup>
                  <FormGroup>
                    <Label for="imageUrl">Gif URL</Label>
                    <Input
                      id="imageUrl"
                      onChange={(e) => setImageUrl(e.target.value)}
                    />
                  </FormGroup>
                  <FormGroup>
                    <Label for="title">Title</Label>
                    <Input id="title" onChange={(e) => setTitle(e.target.value)} />
                  </FormGroup>
                  <FormGroup>
                    <Label for="caption">Caption</Label>
                    <Input
                      id="caption"
                      onChange={(e) => setCaption(e.target.value)}
                    />
                  </FormGroup>
                  <FormGroup>
                    <Label for="dateCreated">dateCreated</Label>
                    <Input
                      id="dateCreated"
                      type="date"
                      onChange={(e) => setDateCreated(e.target.value)}
                    />
                  </FormGroup>
                </Form>
                <Button color="info" onClick={submit}>
                  SUBMIT
                </Button>
              </CardBody>
            </Card>
          </div>
        </div>
      );
    };

export default PostForm;