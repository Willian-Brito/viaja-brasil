import apiClient from '../services/apiClient';

export const createTouristSpot = async data => {
    const response = await apiClient.post(
        '/tourist-spots',
        data
    );

    return response.data;
};

export const getTouristSpot = async id => {
    const response = await apiClient.get(
        `/tourist-spots/${id}`
    );

    return response.data;
};

export const getTouristSpots = async (
    search,
    page,
    pageSize
) => {
    const response = await apiClient.get(
        '/tourist-spots',
        {
            params: {
                search,
                page,
                pageSize
            }
        }
    );

    return response.data;
};