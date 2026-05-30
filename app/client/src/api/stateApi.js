import apiClient from '../services/apiClient';

export const getStates = async () => {
    const response = await apiClient.get('/states');

    return response.data;
};

export const getCities = async state => {
    const response = await apiClient.get(
        `/states/${state}/cities`
    );

    return response.data;
};