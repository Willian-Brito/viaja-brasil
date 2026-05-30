import {
    BrowserRouter,
    Routes,
    Route
} from 'react-router-dom';

import TouristSpotListPage from './pages/TouristSpotListPage';
import TouristSpotCreatePage from './pages/TouristSpotCreatePage';
import TouristSpotDetailsPage from './pages/TouristSpotDetailsPage';

export default function AppRoutes() {
    return (
        <BrowserRouter>
            <Routes>

                <Route
                    path="/"
                    element={
                        <TouristSpotListPage />
                    }
                />

                <Route
                    path="/create"
                    element={
                        <TouristSpotCreatePage />
                    }
                />

                <Route
                    path="/tourist-spots/:id"
                    element={
                        <TouristSpotDetailsPage />
                    }
                />

            </Routes>
        </BrowserRouter>
    );
}