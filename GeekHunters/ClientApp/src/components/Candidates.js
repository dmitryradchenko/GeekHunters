import React, { Component } from 'react';
import { AddCandidateModal } from './AddCandidateModal';
import matchSorter from 'match-sorter';

// Import React Table
import ReactTable from "react-table";
import "react-table/react-table.css";

export class Candidates extends Component {
    displayName = Candidates.name

    constructor(props) {
        super(props);

        this.renderCandidatesTable = this.renderCandidatesTable.bind(this);
        this.updateCandidates = this.updateCandidates.bind(this);
        this.state = {
            candidates: [],
            availableSkills: [],
            loading: true,
            skillsLoaded: false,
            candidatesLoaded: false,
            skillsFilter: []
        };

        fetch('api/candidate/list')
            .then(response => response.json())
            .then(data => {
                this.setState({
                    candidates: data,
                    candidatesLoaded: true,
                    loading: !this.state.skillsLoaded
                });
            });

        fetch('api/candidate/availableSkills')
            .then(response => response.json())
            .then(data => {
                this.setState({
                    availableSkills: data,
                    skillsLoaded: true,
                    loading: !this.state.candidatesLoaded
                });
            });
    }

    noRowsTemplate() {
        return (
            <div>
                <AddCandidateModal availableSkills={this.state.availableSkills} parentUpdate={this.updateCandidates} />
                <br />
                <p>No candidates</p>
            </div>
        );
    }

    handleChangeSelect = (selectedOption) => {
        console.log({ ...this.state.candidate, skills: selectedOption });
        this.setState({ skillsFilter: selectedOption });
    }

    renderCandidatesTable(data) {

        const columns = [
            {
                Header: "Identifier",
                accessor: "id",
                filterMethod: (filter, rows) =>
                    matchSorter(rows, filter.value, { keys: ["id"] }),
                filterAll: true
            },
            {
                Header: "First Name",
                accessor: "firstName",
                filterMethod: (filter, rows) =>
                    matchSorter(rows, filter.value, { keys: ["firstName"] }),
                filterAll: true
            },
            {
                Header: "Last Name",
                id: "lastName",
                accessor: d => d.lastName,
                filterMethod: (filter, rows) =>
                    matchSorter(rows, filter.value, { keys: ["lastName"] }),
                filterAll: true
            },
            {
                Header: "Skills",
                accessor: "skills",
                id: "skills",
                Cell: ({ value, row }) => {
                    return (
                        <div>
                            {value.length > 0 ? value.map(function (elem) { return elem.name; }).join(', ') : ''}
                        </div>
                    );
                },
                filterMethod: (filter, row) => {
                    let skillsList = row.skills.map(function (elem) { return elem.name.toLowerCase(); });
                    if (filter.value === ''
                        || skillsList.filter(s => s.includes(filter.value.toLowerCase())).length > 0) {
                        return true;
                    }
                }
            }
        ];

        return (
            <div>
                <AddCandidateModal parentUpdate={this.updateCandidates} availableSkills={this.state.availableSkills} />
                <br />
                <ReactTable
                    data={data}
                    filterable
                    defaultFilterMethod={(filter, row) =>
                        String(row[filter.id]) === filter.value}
                    columns={columns}
                    defaultPageSize={10}
                    className="-striped -highlight"
                />
            </div>
        );
    }

    updateCandidates() {
        fetch('api/candidate/list')
            .then(response => response.json())
            .then(data => {
                this.setState({ candidates: data });
            });
    }

    render() {
        let contents = this.state.loading
        ? (<p><em>Loading...</em></p>)
        : (this.state.candidates.length === 0 ? this.noRowsTemplate() : this.renderCandidatesTable(this.state.candidates));

        return (
            <div>
                <h1>Candidates table</h1>
                {contents}
            </div>
        );
    }
}
