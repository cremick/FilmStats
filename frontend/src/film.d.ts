export interface FilmSearch {
    adult: boolean;
    backdrop_path: string | null;
    genre_ids: number[];
    id: number;
    original_language: string;
    original_title: string; 
    overview: string;
    popularity: number;
    poster_path: string | null;
    release_date: string;
    title: string;
    video: boolean;
    vote_average: number;
    vote_count: number;
}

export interface FilmProfile {
    adult: boolean;
    backdrop_path: string | null;
    belongs_to_collection: {
        id: number,
        name: string,
        poster_path: string,
        backdrop_path: string
    };
    budget: number;
    genres: {
        id: number,
        name: string
    }[];
    homepage: string;
    id: number;
    imdb_id: string;
    origin_country: string[];
    original_language: string;
    original_title: string;
    overview: string;
    popularity: number;
    poster_path: string;
    production_companies: {
        id: number,
        logo_path: string,
        name: string,
        origin_country: string
    }[];
    production_countries: {
        iso_3166_1: string,
        name: string
    }[];
    release_date: string;
    revenue: number;
    runtime: number;
    spoken_languages: {
        english_name: string,
        iso_639_1: string,
        name: string,
    }[];
    status: string;
    tagline: string;
    title: string;
    video: boolean;
    vote_average: number;
    vote_count: number;
}

interface CastMember {
    adult: boolean;
    gender: number;
    id: number;
    known_for_department: string;
    name: string;
    original_name: string;
    popularity: number;
    profile_path: string;
    cast_id: number;
    character: string;
    credit_id: string;
    order: number;
}

interface CrewMember {
    adult: boolean;
    gender: number;
    id: number;
    known_for_department: string;
    name: string;
    original_name: string;
    popularity: number;
    profile_path: string;
    credit_id: string;
    department: string;
    job: string;
}

export interface FilmCredits {
    cast: CastMember[];
    crew: CrewMember[];
}