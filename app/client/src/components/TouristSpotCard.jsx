import { Link } from 'react-router-dom';

export default function TouristSpotCard({
    touristSpot
}) {
    return (
        <div className="card mb-3 shadow-sm">

            <div className="card-body">

                <h5 className="card-title">
                    {touristSpot.name}
                </h5>

                <div className="text-muted mb-3">
                    📍 {touristSpot.location}
                </div>

                <div>
                    <Link
                        className="btn btn-outline-primary"
                        to={`/tourist-spots/${touristSpot.id}`}
                    >
                        Ver detalhes
                    </Link>
                </div>

            </div>

        </div>
    );
}