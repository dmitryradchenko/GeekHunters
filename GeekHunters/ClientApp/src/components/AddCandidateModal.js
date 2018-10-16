import React from 'react';
import { Button, Modal, Form, FormGroup, ControlLabel, FormControl } from 'react-bootstrap';
import Select from 'react-select';

function FieldGroup({ id, label, ...props }) {
    return (
        <FormGroup controlId={id}>
            <ControlLabel>{label}</ControlLabel>
            <FormControl {...props} />
        </FormGroup>
    );
}

export class AddCandidateModal extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.handleShow = this.handleShow.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.saveCandidate = this.saveCandidate.bind(this);
        this.resetModalValues = this.resetModalValues.bind(this);

        this.state = {
            show: false,
            candidate: {
                firstName: '',
                lastName: '',
                skills: []
            },
            availableSkills: props.availableSkills ? props.availableSkills.map(function (skill) {
                return {
                    value: skill.id,
                    label: skill.name
                };
            }) : []
        };
    }

    resetModalValues() {
        this.setState({
            candidate: {
                firstName: '',
                lastName: '',
                skills: []
            }
        });
    }

    handleClose() {
        this.setState({ show: false });
        this.resetModalValues();
    }

    handleShow() {
        this.setState({ show: true });
    }

    saveCandidate() {
        var self = this;
        let candidate = this.state.candidate;
        var model = {
            firstName: candidate.firstName,
            lastName: candidate.lastName,
            skills: candidate.skills ? candidate.skills.map(function (skill) {
                return skill.label;
            }) : []
        };
        fetch('api/candidate/add',
            {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "POST",
                body: JSON.stringify(model)
            })
            .then(function (res) {
                self.props.parentUpdate();
            });
        this.setState({ show: false });
        this.resetModalValues();
    }

    handleChange = (event) => {
        let fieldName = event.target.name;
        let fieldVal = event.target.value;
        this.setState({ candidate: { ...this.state.candidate, [fieldName]: fieldVal } });
    }

    handleChangeSelect = (selectedOption) => {
        this.setState({ candidate: { ...this.state.candidate, skills: selectedOption } });
    }
    
    render() {
        return (
            <div>
                <Button bsStyle="primary" onClick={this.handleShow}>
                    Add Candidate
                </Button>

                <Modal footer={null} show={this.state.show} onHide={this.handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>Adding candidate</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form onSubmit={this.saveCandidate}>
                            <FieldGroup id="formControlsFirstname"
                                type="text"
                                label="First name"
                                name="firstName"
                                value={this.state.candidate.firstName}
                                onChange={this.handleChange}
                                placeholder="Enter first name" />
                            <FieldGroup id="formControlsLastname"
                                type="text"
                                label="Last name"
                                name="lastName"
                                value={this.state.candidate.lastName}
                                onChange={this.handleChange}
                                placeholder="Enter last name" />
                            <FormGroup controlId={"formSkills"}>
                                <ControlLabel>Skills</ControlLabel>
                                <Select id="formSkills"
                                    label="Skills"
                                    name="skills"
                                    value={this.state.skills}
                                    onChange={this.handleChangeSelect}
                                    options={this.state.availableSkills}
                                    isMulti={true} />
                            </FormGroup>
                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button onClick={this.saveCandidate}>Save</Button>
                        <Button onClick={this.handleClose}>Close</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        );
    }
}

